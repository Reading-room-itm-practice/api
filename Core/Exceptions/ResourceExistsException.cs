using System.Net;

namespace Core.Exceptions
{
    public class ResourceExistsException : ApiException
    {
        public ResourceExistsException(string message) : base(message)
        {
            ResponseCode = HttpStatusCode.BadRequest;
        }
    }
}
