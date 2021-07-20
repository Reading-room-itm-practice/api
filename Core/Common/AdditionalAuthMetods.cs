using Core.Interfaces.Auth;
using Core.ServiceResponses;
using Core.Services.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Storage.Identity;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Core.Common
{
    class AdditionalAuthMetods : BaseAuthServicesProvider, IAdditionalAuthMetods
    {
        public AdditionalAuthMetods(UserManager<User> _userManager, IConfiguration _config, IJwtGenerator _jwtGenerator) 
            : base(_userManager, _config, _jwtGenerator) {}

        public string BuildUrl(string token, string username, string path)
        {
            var uriBuilder = new UriBuilder(path);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["token"] = token;
            query["username"] = username;
            uriBuilder.Query = query.ToString();
            var urlString = uriBuilder.ToString();

            return urlString;
        }

        public string CreateValidationErrorMessage(IdentityResult result)
        {
            StringBuilder builder = new StringBuilder();
            foreach (var error in result.Errors)
            {
                builder.Append(error.Description + " ");
            }

            return builder.ToString();
        }

        public async Task<ServiceResponse> GetUserTokenResponse(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var roles = await _userManager.GetRolesAsync(user);
            return new SuccessResponse<string>
            {
                Message = "Successful login",
                Content = $"{_jwtGenerator.GenerateJWTToken(_config, user, roles)}"
            };
        }
    }
}
