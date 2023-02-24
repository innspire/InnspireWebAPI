using System.Security.Principal;

namespace InnspireWebAPI.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        public bool CanCreateCompanies(IPrincipal principal)
        {
            return principal.IsInRole("Administrator");
        }
    }
}
