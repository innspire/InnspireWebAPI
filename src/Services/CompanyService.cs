using InnspireWebAPI.Entities.Infrastructure;
using InnspireWebAPI.Util;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Security.Claims;
using System.Security.Principal;

namespace InnspireWebAPI.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly IInnspireAuthorizationService authorizationService;

        public CompanyService(IInnspireAuthorizationService authorizationService)
        {
            this.authorizationService = authorizationService;
        }

        public IResult CreateCompany(string name, IPrincipal userPrincipal)
        {
            if (!authorizationService.CanCreateCompanies(userPrincipal))
                return Results.Unauthorized();

            var company = new Company(name);
            return Results.Ok(company);
        }
    }
}
