using InnspireWebAPI.Entities.Authentication;
using InnspireWebAPI.Entities.Infrastructure;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InnspireWebAPI.Entities
{
    public class InnspireDbContext : IdentityDbContext<InnspireUser, InnspireRole, string>
    {
        public DbSet<Company> Companies => Set<Company>();

        public DbSet<HotelChain> HotelChains => Set<HotelChain>();

        public DbSet<Hotel> Hotels => Set<Hotel>();

        public DbSet<Room> Rooms => Set<Room>();

        public DbSet<RoomCategory> RoomCategories => Set<RoomCategory>();

        public DbSet<RoomCateogryConfiguration> RoomCateogryConfigurations=> Set<RoomCateogryConfiguration>();

        public InnspireDbContext(DbContextOptions<InnspireDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RoomCateogryConfiguration>().HasKey(n => new
            {
                n.Room,
                n.RoomCategory
            });

            modelBuilder.Entity<RoomCategoryFeature>().HasKey(n => new
            {
                n.RoomCategoryConfiguration,
                n.Feature
            });

            modelBuilder.Entity<CompanyUser>().HasKey(n => new
            {
                n.Company,
                n.User
            });
        }
    }
}
