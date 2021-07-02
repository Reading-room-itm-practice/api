using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Common;
using WebAPI.Models;

namespace WebAPI.Interfaces
{
    public interface IDeleter<T> where T : IDbModel
    {
        public Task Delete(int id);
    }
}
