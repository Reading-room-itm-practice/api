using Core.ServiceResponses;
using System;
using System.Threading.Tasks;

namespace Core.Interfaces.Profile
{
    public interface IProfileService
    {
        public Task<ServiceResponse> GetProfile(Guid? id);
    }
}
