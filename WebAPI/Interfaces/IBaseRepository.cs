using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Interfaces
{
    public interface IBaseRepository
    {
        public Task<T> Create<T>(T author);
        public Task Edit<T>(T author);
        public Task Delete<T>(T author);
    }
}
