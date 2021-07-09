using Core.Common;
using Core.DTOs;
using System.Collections.Generic;

namespace Core.ServiceResponses
{
    public class SuccessResponse<T> : ServiceResponse<T>
    {
        public SuccessResponse()
        {
            Success = true;
        }
    }
}
