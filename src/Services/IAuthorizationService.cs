using System.Security.Principal;

namespace InnspireWebAPI.Services
{
    public interface IInnspireAuthorizationService
    {
        bool CanCreateCompanies(IPrincipal principal);
    }
}