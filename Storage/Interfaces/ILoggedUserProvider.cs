using System;

namespace Storage.Interfaces
{
    public interface ILoggedUserProvider
    {
        public Guid GetUserId();
    }
}
