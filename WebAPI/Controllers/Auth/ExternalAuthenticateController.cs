using Core.DTOs;
using Core.Interfaces.Auth;
using Core.Requests;
using Core.ServiceResponses;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebAPI.Controllers.Auth
{

    [Route("api/[controller]")]
    [ApiController]
    public class ExternalAuthenticateController : ControllerBase
    {
        private readonly IExternalLoginService _externalLoginService;

        public ExternalAuthenticateController(IExternalLoginService externalLoginService)
        {
            _externalLoginService = externalLoginService;
        }

        [HttpGet]
        [Route("External-login")]
        public IActionResult ExternalLogin(string provider)
        {
            return _externalLoginService.Request(provider);
        }

        [HttpGet]
        [Route("External-response")]
        public async Task<ServiceResponse> ExternalResponse()
        {
            return await _externalLoginService.Login();
        }

    }
}