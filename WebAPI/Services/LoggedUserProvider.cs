using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Storage.Interfaces;
using System.Security.Claims;

namespace WebAPI.Services
{
    public class LoggedUserProvider : ILoggedUserProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private const string ID_IDENTIFIER = "Id";

        public LoggedUserProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int GetUserId()
        {
            var loggedUserId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ID_IDENTIFIER);
          
            return loggedUserId != null ? int.Parse(loggedUserId) : 0;
        }
    }
}
