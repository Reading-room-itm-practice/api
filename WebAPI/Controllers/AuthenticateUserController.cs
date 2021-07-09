using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Core.Interfaces;
using Core.DTOs;
using Core.ServiceResponses;

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
        public async Task<SuccessResponse<string>> Login([FromBody] LoginRequest model)
        {
            return await _authenticationUserService.Login(model);
        }

        [HttpPost]
        [Route("register")]
        public async Task<SuccessResponse<string>> Register([FromBody] RegisterRequest model)
        {
            return await _authenticationUserService.Register(model);
        }

        [HttpPost]
        [Route("register-admin")]
        public async Task<SuccessResponse<string>> RegisterAdmin([FromBody] RegisterRequest model)
        {
            return await _authenticationUserService.RegisterAdmin(model);
        }
    }
}