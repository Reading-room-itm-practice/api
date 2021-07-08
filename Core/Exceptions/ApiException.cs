using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Common;

namespace Core.Exceptions
{
    public abstract class ApiException : Exception
    {
        public static int ResponseCode { get; protected set; }

        public ApiException(string message) : base(message)
        {
        }
    }
}
