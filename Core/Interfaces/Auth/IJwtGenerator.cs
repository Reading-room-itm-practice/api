using Microsoft.Extensions.Configuration;
using Storage.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Auth
{
    public interface IJwtGenerator
    {
        public string GenerateJWTToken(IConfiguration _config, User user, IList<string> roles);

    }
}
