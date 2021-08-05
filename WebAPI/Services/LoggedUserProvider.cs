using Microsoft.AspNetCore.Http;
using Storage.Interfaces;
using System;
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

        public Guid GetUserId()
        {
            var loggedUserId = "122184f3-87b6-4908-2cae-08d957e51711";// _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

            return loggedUserId != null ? new Guid(loggedUserId) : throw new UnauthorizedAccessException();
        }
    }
}
