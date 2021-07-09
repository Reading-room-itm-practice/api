using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Core.DTOs;
using Core.Interfaces;

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
        public async Task<ResponseDto> Login([FromBody] LoginDto model)
        {
            return await _authenticationUserService.Login(model);
        }

        [HttpPost]
        [Route("register")]
        public async Task<ResponseDto> Register([FromBody] RegisterDto model)
        {
            return await _authenticationUserService.Register(model);
        }

        [HttpPost]
        [Route("register-admin")]
        public async Task<ResponseDto> RegisterAdmin([FromBody] RegisterDto model)
        {
            return await _authenticationUserService.RegisterAdmin(model);
        }

        [HttpPost]
        [Route("confirmemail")]
        public async Task<ResponseDto> ConfirmEmail(string token, string username)
        {
            ConfirmEmailModel model = new(){Token = token, UserName = username};

            return await _authenticationUserService.ConfirmEmail(model);
        }
    }
}