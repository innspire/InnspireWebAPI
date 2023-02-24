using InnspireWebAPI.Entities;
using InnspireWebAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
InternalConfigurationsManager.Create(builder.Configuration);
// Add services to the container.

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<InnspireDbContext>(opt =>
    {
        opt.UseSqlite("Data Source=local_dev.db");
    }); 
}

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHealthChecks()
    .AddCheck<BasicHealthCheck>("Base");

builder.Services.AddScoped<IInnspireAuthorizationService, AuthorizationService>();

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(opt =>
{
    opt.SaveToken = true;
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = InternalConfigurationsManager.Instance!.ApiUrl,
        ValidAudience = InternalConfigurationsManager.Instance!.ApiHostname,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(InternalConfigurationsManager.Instance!.JwtKey))
    };
});


builder.Services.AddIdentityCore<InnspireUser>(opt =>
{
    if (builder.Environment.IsDevelopment())
    {
        opt.Password.RequiredLength = 1;
        opt.Password.RequireDigit = false;
        opt.Password.RequiredUniqueChars = 1;
        opt.Password.RequireNonAlphanumeric= false;
        opt.Password.RequireLowercase= false;
        opt.Password.RequireUppercase= false;
    }
    // No options for now
    
});

new IdentityBuilder(typeof(InnspireUser), typeof(InnspireRole), builder.Services)
    .AddRoleManager<RoleManager<InnspireRole>>()
    .AddSignInManager<SignInManager<InnspireUser>>()
    .AddEntityFrameworkStores<InnspireDbContext>();

builder.Services.AddScoped<JwtService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    var dbContext = app.Services.CreateScope().ServiceProvider.GetService<InnspireDbContext>();

    if (dbContext != null)
    {
        dbContext.Database.EnsureCreated();
        dbContext.Database.Migrate();
    }
    else
    {
        app.Logger.LogWarning("No database context configured");
    }
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseHealthServer();

MetricsHelper.ActivateExporting();

app.Run();
