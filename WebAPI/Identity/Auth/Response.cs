using System.Net;

namespace WebAPI.Models.Auth
{
    public class Response
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public bool isAdmin { get; set; }
    }
}