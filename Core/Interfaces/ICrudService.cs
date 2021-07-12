using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Common;
using Storage.Iterfaces;

namespace Core.Interfaces
{
    public interface ICrudService<T> : ICreatorService<T>, IGetterService<T>, IUpdaterService<T>, IDeleterService<T> where T : IDbModel
    {
    }
}
