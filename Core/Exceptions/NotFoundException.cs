using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Exceptions
{
    public class NotFoundException : ApiException
    {
        public NotFoundException(string message) : base(message)
        {
            ResponseCode = StatusCodes.Status404NotFound;
        }
    }
}
