using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Common;
using Storage.Iterfaces;

namespace Core.Interfaces
{
    public interface IUpdaterService<T> where T : IDbModel
    {
        public Task Update(IRequest updateModel, int id);
    }
}
