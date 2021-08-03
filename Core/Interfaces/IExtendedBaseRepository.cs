using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Storage.Interfaces;

namespace Core.Interfaces
{
    public interface IExtendedBaseRepository<T> : IBaseRepository<T> where T : class, IDbModel
    {
        public Task<IEnumerable<T>> FindByConditionsWithIncludes(Expression<Func<T, bool>> expression,
            params string[] includes);

        public Task<IEnumerable<T>> FindAllWithIncludes(params string[] includes);
    }
}
