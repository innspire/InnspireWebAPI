using InnspireWebAPI.Entities.Authentication;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace InnspireWebAPI.Entities.Infrastructure
{
    /// <summary>
    /// A company can manage different Hotel Chains
    /// </summary>
    public sealed class Company
    {
        public string CompanyId { get; set; }

        public string CompanyName { get; set; }

        public List<HotelChain>? HotelChains { get; set; }

        public ICollection<CompanyRole>? Roles { get; set; }

        public ICollection<CompanyUser>? Users { get; set; }

        public Company(string companyId, string companyName)
        {
            if (string.IsNullOrEmpty(companyId))
            {
                CompanyId = Guid.NewGuid().ToString();
            }
            else
            {
                CompanyId = companyId;
            }
            CompanyName = companyName;
        }

        public Company(string companyName)
        {
            CompanyId = Guid.NewGuid().ToString();
            CompanyName = companyName;
        }
    }

    public sealed class CompanyUser
    {
        public string CompanyId { get; set; }
        public string UserId { get; set; }

        [ForeignKey(nameof(CompanyId))]
        public Company? Company { get; set; }

        [ForeignKey(nameof(UserId))]
        public InnspireUser? User { get; set; }

        public CompanyRole? Role { get; set; }

        public CompanyUser(string companyId, string userId)
        {
            this.CompanyId = companyId;
            this.UserId = userId;
        }
    }

    public sealed class CompanyRole
    {
        public string CompanyRoleId { get; set; }

        public string CompanyId { get; set; }

        public Company? Company { get; set; }


        public CompanyRole(string companyId)
        {
            this.CompanyRoleId= Guid.NewGuid().ToString();
            this.CompanyId= companyId;
        }
    }

    /// <summary>
    /// A hotel chain is a kind of brand that a company holds.
    /// </summary>
    public sealed class HotelChain
    {
        public string HotelChainId { get; set; }

        public string HotelChainName { get; set; }

        public Company? Company { get; set; }

        public List<Hotel>? Hotels { get; set; }

        public HotelChain(string hotelChainName)
        {
            HotelChainId = Guid.NewGuid().ToString();
            HotelChainName = hotelChainName;
        }
    }

    /// <summary>
    /// A hotel is an instance of a house/hotel that belongs to a chain.
    /// </summary>
    public sealed class Hotel
    {
        public string HotelId { get; set; }

        public List<Room>? Rooms { get; set; }

        public Hotel()
        {
            HotelId = Guid.NewGuid().ToString();
        }
    }

    /// <summary>
    /// A room is a a physical existing object inside a hotel.
    /// </summary>
    public sealed class Room
    {
        public string RoomId { get; set; }

        public Hotel? Hotel { get; set; }

        public Room()
        {
            RoomId = Guid.NewGuid().ToString();
        }
    }

    public sealed class RoomCateogryConfiguration
    {
        [Key]
        public string RoomCategoryConfigurationId { get; set; }

        public string RoomId { get; set; }
        public string CategoryId { get; set; }


        [ForeignKey(nameof(RoomId))]
        public Room? Room { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public RoomCategory? RoomCategory { get; set; }

        public RoomCateogryConfiguration(string roomId, string categoryId)
        {
            this.RoomCategoryConfigurationId = Guid.NewGuid().ToString();
            this.RoomId = roomId;
            this.CategoryId = categoryId;
        }

    }

    public sealed class RoomCategoryFeature
    {
        public string ConfigurationId { get; set; }

        public string FeatureId { get; set; }

        [ForeignKey(nameof(ConfigurationId))]
        public RoomCateogryConfiguration? RoomCategoryConfiguration { get; set; }

        [ForeignKey(nameof(FeatureId))]
        public Feature? Feature { get; set; }

        public bool IsAvailable { get; set; }

        public bool IsBookable { get; set; }

        public decimal? Price { get; set; }


        public RoomCategoryFeature(string configurationId, string featureId)
        {
            this.ConfigurationId = configurationId;
            this.FeatureId = featureId;
        }

    }

    public sealed class Feature
    {
        public string FeatureId { get; set; }

        public Feature()
        {
            this.FeatureId= Guid.NewGuid().ToString();
        }
    }

    public sealed class RoomCategory
    {
        public string RoomCategoryId { get; set; }

        public RoomCategory()
        {
            RoomCategoryId = Guid.NewGuid().ToString();
        }
    }

}
