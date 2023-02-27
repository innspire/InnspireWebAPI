using InnspireWebAPI.Entities.Authentication;
using InnspireWebAPI.Entities.Infrastructure;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InnspireWebAPI.Entities
{
    public class InnspireDbContext : IdentityDbContext<InnspireUser, InnspireRole, string, InnspireUserClaim, InnspireUserRole, InnspireUserLogin, InnspireRoleClaim, InnspireUserToken >
    {
        public DbSet<Company> Companies => Set<Company>();

        public DbSet<HotelChain> HotelChains => Set<HotelChain>();

        public DbSet<Hotel> Hotels => Set<Hotel>();

        public DbSet<Room> Rooms => Set<Room>();

        public DbSet<RoomCategory> RoomCategories => Set<RoomCategory>();

        public DbSet<RoomCateogryConfiguration> RoomCateogryConfigurations=> Set<RoomCateogryConfiguration>();

        public DbSet<CompanyUser> CompanyUsers => Set<CompanyUser>();

        public InnspireDbContext(DbContextOptions<InnspireDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<RoomCategoryFeature>().HasKey(n => new
            {
                n.ConfigurationId,
                n.FeatureId
            });

            modelBuilder.Entity<CompanyUser>().HasKey(n => new
            {
                n.CompanyId,
                n.UserId
            });

            modelBuilder.Entity<InnspireUserLogin>().HasKey(n => new
            {
                n.UserId,
                n.LoginProvider,
                n.ProviderKey,
                n.Timestamp
            });

            modelBuilder.Entity<InnspireUserRole>().HasKey(n => new
            {
                n.UserId,
                n.RoleId
            });

            modelBuilder.Entity<InnspireUserToken>().HasKey(n => n.Value);
        }
    }
}
