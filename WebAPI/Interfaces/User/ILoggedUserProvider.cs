using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Interfaces.User
{
    public interface ILoggedUserProvider
    {
        int UserId { get; }
    }
}
