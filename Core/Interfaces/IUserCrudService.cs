using Core.Common;
using Storage.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IUserCrudService<T> : ICrudService<T> where T : class, IApproveable, IDbMasterKey<int>
    {
    }
}
