using Microsoft.AspNetCore.Identity;
using System;
using System.Text;
using System.Web;

namespace Core.Common
{
    class AdditionalAuthMetods
    {
        public static string BuildUrl(string token, string username, string path)
        {
            var uriBuilder = new UriBuilder(path);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["token"] = token;
            query["username"] = username;
            uriBuilder.Query = query.ToString();
            var urlString = uriBuilder.ToString();

            return urlString;
        }

        public static string CreateValidationErrorMessage(IdentityResult result)
        {
            StringBuilder builder = new StringBuilder();
            foreach (var error in result.Errors)
            {
                builder.Append(error.Description + " ");
            }

            return builder.ToString();
        }
    }
}
