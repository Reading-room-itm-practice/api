using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Core.Interfaces.Auth;
using Core.DTOs;
using Core.ServiceResponses;
using Core.Requests;

namespace WebAPI.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IAuthenticationService _authenticateService;
        private readonly IGoogleService _googleService;

        public AuthenticateController(IAuthenticationService authenticationService, IGoogleService googleService)
        {
            _authenticateService = authenticationService;
            _googleService = googleService;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ServiceResponse> Login([FromBody] LoginRequest model)
        {
            return await _authenticateService.Login(model);
        }

        [HttpGet]
        [Route("Google-login")]
        public IActionResult GoogleLogin()
        {
            return new ChallengeResult("Google", _googleService.GoogleRequest()); 
        }

        [HttpGet]
        [Route("Google-response")]
        public async Task<ServiceResponse> GoogleResponse()
        {
            return await _googleService.Login();
        }

        [HttpPost]
        [Route("Register")]
        public async Task<ServiceResponse> Register([FromBody] RegisterRequest model)
        {
            return await _authenticateService.Register(model);
        }

        [HttpGet]
        [Route("Confirm-email")]
        public async Task<ServiceResponse> ConfirmEmail(string token, string username)
        {
            EmailDto model = new() {Token = token, UserName = username};

            return await _authenticateService.ConfirmEmail(model);
        }

        [HttpPost]
        [Route("Forgot-password")]
        public async Task<ServiceResponse> ForgotPassword(string email)
        {
           return await _authenticateService.SendResetPasswordEmail(email);
        }

        [HttpPost]
        [Route("Reset-password")]
        public async Task<ServiceResponse> ResetPassword([FromBody] ResetPasswordRequest model)
        {
            return await _authenticateService.ResetPassword(model);
        }
    }
}