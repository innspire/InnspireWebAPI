using InnspireWebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InnspireWebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        [HttpGet("[action]")]
        public string GetData()
        {
            return "success";
        }

        [AllowAnonymous]
        [HttpGet("[action]")]
        public string GetAnyone()
        {
            return "success";
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        public string Login([FromServices]JwtService jwtService)
        {
            var token = jwtService.Authenticate("");
            return token.ToString();
        }
    }
}
