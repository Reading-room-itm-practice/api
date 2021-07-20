using Core.ServiceResponses;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Core.Interfaces.Auth
{
    interface IAdditionalAuthMetods
    {
        public string BuildUrl(string token, string username, string path);

        public string CreateValidationErrorMessage(IdentityResult result);

        public Task<ServiceResponse> GetUserTokenResponse(string email);
    }
}
