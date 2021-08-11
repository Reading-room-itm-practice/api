using Core.Response;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Core.Interfaces.Profile
{
    public interface IProfileService
    {
        public Task<ServiceResponse> GetProfile(Guid? id);

        public Task<ServiceResponse> UpdatePhoto(IFormFile photo);
    }
}
