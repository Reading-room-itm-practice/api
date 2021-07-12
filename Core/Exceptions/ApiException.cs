using System;
using System.Net;

namespace Core.Exceptions
{
    public abstract class ApiException : Exception
    {
        public static HttpStatusCode ResponseCode { get; protected set; }

        public ApiException(string message) : base(message)
        {
        }
    }
}
