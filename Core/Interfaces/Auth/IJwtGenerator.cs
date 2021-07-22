using Microsoft.Extensions.Configuration;
using Storage.Identity;
using System.Collections.Generic;

namespace Core.Interfaces.Auth
{
    public interface IJwtGenerator
    {
        public string GenerateJWTToken(IConfiguration _config, User user, IList<string> roles);

    }
}
