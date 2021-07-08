using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebAPI.Identity.Auth;
using WebAPI.Models.Auth;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateUserController : ControllerBase
    {
        private readonly IUserAuthenticationService _authenticationUserService;

        public AuthenticateUserController(IUserAuthenticationService authenticationService)
        {
            _authenticationUserService = authenticationService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<Response> Login([FromBody] LoginModel model)
        {
            return await _authenticationUserService.Login(model);
        }

        [HttpPost]
        [Route("register")]
        public async Task<Response> Register([FromBody] RegisterModel model)
        {
            return await _authenticationUserService.Register(model);
        }

        [HttpPost]
        [Route("register-admin")]
        public async Task<Response> RegisterAdmin([FromBody] RegisterModel model)
        {
            return await _authenticationUserService.RegisterAdmin(model);
        }

        [HttpPost]
        [Route("confirmemail")]
        public async Task<Response> ConfirmEmail(string token, string username)
        {
            ConfirmEmailModel model = new()
            {
                Token = token,
                UserName = username
            };
            return await _authenticationUserService.ConfirmEmail(model);
        }
    }
}