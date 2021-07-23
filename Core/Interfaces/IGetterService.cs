using Storage.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IGetterService<T> where T : IDbModel
    {
        public Task<IEnumerable<IDto>> GetAll<IDto>();
        public Task<IDto> GetById<IDto>(int id);
    }
}
