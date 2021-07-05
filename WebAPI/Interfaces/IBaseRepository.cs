using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebAPI.Common;

namespace WebAPI.Interfaces
{
    public interface IBaseRepository<T> where T : class, IDbModel
    {
        public Task<T> Create(T model);
        public Task Delete(T model);
        public Task Edit(T model);
        public Task<IEnumerable<T>> FindAll();
        public Task<IEnumerable<T>> FindByConditions(Expression<Func<T, bool>> expresion);
    }
}
