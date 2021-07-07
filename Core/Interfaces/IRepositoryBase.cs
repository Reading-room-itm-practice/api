using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IRepositoryBase<T>
    {
        IQueryable<T> FindAll();
        IQueryable<T> FindByConditions(Expression<Func<T, bool>> expresion);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);

    }
}
