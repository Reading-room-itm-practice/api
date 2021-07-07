using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Common;
using Core.Models;

namespace Core.Interfaces
{
    public interface IDeleterService<T> where T : IDbModel
    {
        public Task Delete(int id);
    }
}
