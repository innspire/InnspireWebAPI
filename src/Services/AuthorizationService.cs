using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Security.Principal;

namespace InnspireWebAPI.Services
{
    public class AuthorizationService : IInnspireAuthorizationService, IAuthorizationService
    {
        public Task<AuthorizationResult> AuthorizeAsync(ClaimsPrincipal user, object? resource, IEnumerable<IAuthorizationRequirement> requirements)
        {
            throw new NotImplementedException();
        }

        public Task<AuthorizationResult> AuthorizeAsync(ClaimsPrincipal user, object? resource, string policyName)
        {
            throw new NotImplementedException();
        }

        public bool CanCreateCompanies(IPrincipal principal)
        {
            return principal.IsInRole("Administrator");
        }
    }
}
