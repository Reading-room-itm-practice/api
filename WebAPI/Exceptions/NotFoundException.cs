using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Exceptions
{
    public class NotFoundException : ApiException
    {
        public NotFoundException(string message) : base(StatusCodes.Status404NotFound, message)
        {
        }
    }
}
