using Core.Interfaces.Profile;
using Core.ServiceResponses;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : Controller
    {
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        [SwaggerOperation(Summary = "Retrieves profile")]
        [HttpGet]
        public async Task<ServiceResponse> Profile(Guid? id )
        {
            return await _profileService.GetProfile(id);
        }
    }
}
