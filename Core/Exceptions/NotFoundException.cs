using System.Net;

namespace Core.Exceptions
{
    public class NotFoundException : ApiException
    {
        public NotFoundException(string message) : base(message)
        {
            ResponseCode = HttpStatusCode.NotFound;
        }
    }
}
