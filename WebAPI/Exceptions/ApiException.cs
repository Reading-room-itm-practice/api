using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Common;

namespace WebAPI.Exceptions
{
    public abstract class ApiException : Exception
    {
        public static int ResponseCode { get; protected set; }

        public ApiException(string message) : base(message)
        {
        }
    }
}
