using System.Net;

namespace Core.Response
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
