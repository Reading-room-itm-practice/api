﻿using Core.Interfaces.Profile;
using Core.Response;
using Microsoft.AspNetCore.Http;
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
        [HttpGet("Profile")]
        public async Task<ServiceResponse> Index(Guid? id )
        {
            return await _profileService.GetProfile(id);
        }

        [SwaggerOperation(Summary = "Edits pprofile photo")]
        [HttpPut("Profile-photo-edit")]
        public async Task<ServiceResponse> EditPhoto(IFormFile photo)
        {
            return await _profileService.UpdatePhoto(photo);
        }
    }
}
