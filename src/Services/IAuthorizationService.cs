using System.Security.Principal;

namespace InnspireWebAPI.Services
{
    public interface IAuthorizationService
    {
        bool CanCreateCompanies(IPrincipal principal);
    }
}