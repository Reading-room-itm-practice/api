using Core.DTOs;
using Core.Interfaces.Auth;
using Core.Requests;
using Core.Response;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace WebAPI.Controllers.Auth
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IAuthenticationService _authenticateService;

        public AuthenticateController(IAuthenticationService authenticationService)
        {
            _authenticateService = authenticationService;
        }

        [SwaggerOperation(Summary = "Generate jwt token that identify user")]
        [HttpPost("Login")]
        public async Task<ServiceResponse> Login([FromBody] LoginRequest model)
        {
            return await _authenticateService.Login(model);
        }

        [SwaggerOperation(Summary = "Register new user and send confirmation email")]
        [HttpPost("Register")]
        public async Task<ServiceResponse> Register([FromBody] RegisterRequest model)
        {
            return await _authenticateService.Register(model);
        }

        [SwaggerOperation(Summary = "Confirm email for specific user")]
        [HttpGet("ConfirmEmail")]
        public async Task<ServiceResponse> ConfirmEmail(string token, string username)
        {
            EmailDto model = new() { Token = token, UserName = username };

            return await _authenticateService.ConfirmEmail(model);
        }

        [SwaggerOperation(Summary = "Send and email containing link to change password")]
        [HttpPost("ForgotPassword")]
        public async Task<ServiceResponse> ForgotPassword(string email)
        {
            return await _authenticateService.SendResetPasswordEmail(email);
        }

        [SwaggerOperation(Summary = "Change password for specific user")]
        [HttpPost("ResetPassword")]
        public async Task<ServiceResponse> ResetPassword([FromBody] ResetPasswordRequest model)
        {
            return await _authenticateService.ResetPassword(model);
        }
    }
}