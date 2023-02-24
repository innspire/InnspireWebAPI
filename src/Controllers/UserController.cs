using InnspireWebAPI.DataTransferObjects.User;
using InnspireWebAPI.Entities;
using InnspireWebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
        public async Task<string> Login([FromServices]JwtService jwtService, [FromBody]LoginRequest login)
        {
            var token = await jwtService.Authenticate(login);
            return token ?? "error";
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<string> Register([FromServices] UserManager<InnspireUser> userManager, [FromBody]LoginRequest request)
        {
            var newUser = new InnspireUser()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = request.UserName,
            };

            var identity = await userManager.CreateAsync(newUser, request.Password);
            return $"{identity.Succeeded} -{string.Join(",", identity.Errors.Select(n => $"{n.Code} . {n.Description}"))}";
        }
    }
}
