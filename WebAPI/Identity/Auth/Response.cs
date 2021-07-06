
using System.Net;

namespace WebAPI.Models.Auth
{
    public enum Replay { Yes, No }
    public class Response
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public Replay isAdmin { get; set; }
    }
}
