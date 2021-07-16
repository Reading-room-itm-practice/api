using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Storage.Interfaces;
using System.Security.Claims;

namespace WebAPI.Services
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
            var loggedUserId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

            return 1;//loggedUserId != null ? int.Parse(loggedUserId) : 0;
        }
    }
}
