using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Storage.Interfaces
{
    public interface ILoggedUserProvider
    {
        public Guid GetUserId();
    }
}
