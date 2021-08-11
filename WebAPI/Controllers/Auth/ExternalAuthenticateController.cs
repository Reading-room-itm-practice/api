using Core.Interfaces.Auth;
using Core.Response;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
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

        [SwaggerOperation(Summary = "Redirect to specyfic login provider")]
        [HttpGet("ExternalLogin")]
        public IActionResult ExternalLogin(string provider)
        {
            return _externalLoginService.Request(provider);
        }

        [SwaggerOperation(Summary = "Gets external login information to generate jwt token")]
        [HttpGet("ExternalResponse")]
        public async Task<ServiceResponse> ExternalResponse()
        {
            return await _externalLoginService.Login();
        }

    }
}