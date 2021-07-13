using Core.Common;
using Core.DTOs;
using System.Collections.Generic;
using System.Net;

namespace Core.ServiceResponses
{
    public class SuccessResponse : ServiceResponse
    {
        public SuccessResponse()
        {
            Success = true;
            StatusCode = HttpStatusCode.OK;
        }
    }

    public class SuccessResponse<T> : SuccessResponse
    {
        public T Content { get; set; }
    }
}
