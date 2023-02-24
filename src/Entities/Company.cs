using Microsoft.AspNetCore.Identity;

namespace InnspireWebAPI.Entities
{
    /// <summary>
    /// A company can manage different Hotel Chains
    /// </summary>
    public sealed class Company
    {
        public string CompanyId { get; set; }

        public string CompanyName { get; set; }

        public List<HotelChain>? HotelChains { get; set; }

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
            this.CompanyId = Guid.NewGuid().ToString();
            this.CompanyName = companyName;
        }
    }

    public sealed class HotelChain
    {
        public string HotelChainId { get; set; }

        public string HotelChainName { get; set; }

        public Company? Company { get; set; }

        public List<Hotel>? Hotels { get; set; }

        public HotelChain(string hotelChainName)
        {
            this.HotelChainId = Guid.NewGuid().ToString();
            this.HotelChainName= hotelChainName;
        }
    }

    public sealed class Hotel
    {

    }

    public sealed class Room
    {

    }

    public sealed class RoomCategory
    {

    }

    public class InnspireUser : IdentityUser<string>
    {

    }

    public class InnspireRole : IdentityRole<string>
    {

    }

}
