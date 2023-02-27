using Microsoft.AspNetCore.Identity;

namespace InnspireWebAPI.Entities.Authentication
{
    public class InnspireRole : IdentityRole<string>
    {

    }


    public class InnspireUserClaim : IdentityUserClaim<string> { }

    public class InnspireUserRole : IdentityUserRole<string> { }

    public class InnspireUserLogin : IdentityUserLogin<string> 
    {
        public DateTime Timestamp { get; set; }
    }

    public class InnspireRoleClaim : IdentityRoleClaim<string> { }

    public class InnspireUserToken : IdentityUserToken<string> { }
}
