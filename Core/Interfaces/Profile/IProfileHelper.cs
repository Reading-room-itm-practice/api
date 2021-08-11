using Core.DTOs;
using Core.Response;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Storage.Interfaces
{
    public interface IProfileHelper
    {
        public Task<UserProfile> GetUserProfile(Guid? id);

        public Task<ServiceResponse> EditPhoto(IFormFile image);
    }
}
