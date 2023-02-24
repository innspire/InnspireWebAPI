using System.Security.Principal;

namespace InnspireWebAPI.Services
{
    public interface ICompanyService
    {
        IResult CreateCompany(string name, IPrincipal userPrincipal);
    }
}