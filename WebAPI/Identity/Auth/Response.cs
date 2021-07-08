
namespace WebAPI.Models.Auth
{
    public class Response
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public bool isAdmin { get; set; }
    }
}
