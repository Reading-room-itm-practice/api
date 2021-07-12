using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Core.Interfaces;
using Core.DTOs;
using Core.ServiceResponses;
using Core.Requests;

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
        public async Task<ServiceResponse> Login([FromBody] LoginRequest model)
        {
            return await _authenticationUserService.Login(model);
        }

        [HttpPost]
        [Route("register")]
        public async Task<ServiceResponse> Register([FromBody] RegisterRequest model)
        {
            return await _authenticationUserService.Register(model);
        }

        [HttpGet]
        [Route("Confirm-email")]
        public async Task<ServiceResponse> ConfirmEmail(string token, string username)
        {
            EmailDto model = new(){Token = token, UserName = username};

            return await _authenticationUserService.ConfirmEmail(model);
        }

        [HttpPost]
        [Route("Reset-password-page")]
        public async Task<ResponseDto> ResetPasswordPage(string email)
        {
           return await _authenticationUserService.SendResetPasswordEmail(email);
        }

        [HttpGet]
        [Route("Reset-password")]
        public IActionResult ResetPassword(string token, string username)
        {
            return Ok(token + " " + username);
        }

        [HttpPost]
        [Route("Reset-password-done")]
        public async Task<ResponseDto> ResetPassword([FromBody] ResetPasswordDto model)
        {
            return await _authenticationUserService.ResetPassword(model);
        }
    }
}