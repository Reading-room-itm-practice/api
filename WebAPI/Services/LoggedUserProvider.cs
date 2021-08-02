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
            var loggedUserId = "59c90135-bd67-4f8d-015b-08d95589762d";//_httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

            return loggedUserId != null ? new Guid(loggedUserId) : throw new UnauthorizedAccessException();
        }
    }
}
