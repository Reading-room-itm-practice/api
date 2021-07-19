using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Core.DTOs;
using Core.ServiceResponses;
using Core.Requests;
using Core.Interfaces.Auth;

namespace WebAPI.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateUserController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationUserService;

        public AuthenticateUserController(IAuthenticationService authenticationService)
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
            EmailDto model = new() {Token = token, UserName = username};

            return await _authenticationUserService.ConfirmEmail(model);
        }

        [HttpPost]
        [Route("Forgot-password")]
        public async Task<ServiceResponse> ForgotPassword(string email)
        {
           return await _authenticationUserService.SendResetPasswordEmail(email);
        }

        [HttpPost]
        [Route("Reset-password")]
        public async Task<ServiceResponse> ResetPassword([FromBody] ResetPasswordRequest model)
        {
            return await _authenticationUserService.ResetPassword(model);
        }
    }
}