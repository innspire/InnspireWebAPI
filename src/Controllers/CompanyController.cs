using InnspireWebAPI.DataTransferObjects.Company;
using InnspireWebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InnspireWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        [HttpPost]
        [Authorize]
        public IResult CreateCompany([FromServices]ICompanyService companyService, [FromBody]CreateCompanyRequest request)
        {
            return companyService.CreateCompany(request.CompanyName, User);
        }
    }
}
