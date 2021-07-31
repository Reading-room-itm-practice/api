using Core.DTOs;
using Core.Services;
using Storage.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IBaseRepository<T> where T : class, IDbModel
    {
        public Task<T> Create(T model);
        public Task Delete(T model);
        public Task Edit(T model);
        public Task<DataDto<IEnumerable<T>>> FindAll(PaginationFilter filter);
        public Task<IEnumerable<T>> FindByConditions(Expression<Func<T, bool>> expresion);
    }
}
