using System.Net;
using System.Threading.Tasks;

namespace Core.ServiceResponses
{
    public abstract class ServiceResponse
    {
        public bool Success { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
    }
}
