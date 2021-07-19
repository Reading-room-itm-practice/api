using Core.Requests;
using Core.ServiceResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Auth
{
    public interface ILoginService
    {
        public Task<ServiceResponse> Login(LoginRequest model);
    }
}
