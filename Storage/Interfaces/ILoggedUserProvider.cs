using System;
using System.Security.Claims;

namespace Storage.Interfaces
{
    public interface ILoggedUserProvider
    {
        public Guid GetUserId();
        public ClaimsPrincipal GetUserClaimsPrincipal();
    }
}
