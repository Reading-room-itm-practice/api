using System.Security.Claims;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Core.Services
{
    public class LoggedUserProvider : ILoggedUserProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoggedUserProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int GetUserId()
        {
            var loggedUserId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier).Value;
          
            return loggedUserId != null ? int.Parse(loggedUserId) : 0;
        }
    }
}
