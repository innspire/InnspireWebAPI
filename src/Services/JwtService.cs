using InnspireWebAPI.DataTransferObjects.User;
using InnspireWebAPI.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace InnspireWebAPI.Services
{
    public class JwtService
    {
        private readonly SignInManager<InnspireUser> signInManager;
        private readonly UserManager<InnspireUser> userManager;

        public async Task<string?> Authenticate(LoginRequest login)
        {
            var user = await userManager.FindByNameAsync(login.UserName);
            if (user == null)
                return null;

            var passwordValid = await userManager.CheckPasswordAsync(user, login.Password);

            if (!passwordValid) return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(InternalConfigurationsManager.Instance!.JwtKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
              {
             new Claim(ClaimTypes.Name, login.UserName)
              }),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var myToken = tokenHandler.WriteToken(token);
            return myToken;
        }

        public JwtService(SignInManager<InnspireUser> signInManager, UserManager<InnspireUser> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }
    }
}
