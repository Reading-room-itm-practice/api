using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Exceptions
{
    public abstract class ApiException : Exception
    {
        public int ResponseCode { get; init; }
        public ApiException(int responseCode, string message) : base(message)
        {
            ResponseCode = responseCode;
        }
    }
}
