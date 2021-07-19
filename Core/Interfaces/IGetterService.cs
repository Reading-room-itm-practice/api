using Storage.Iterfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IGetterService<T> where T : IDbModel
    {
        public Task<IEnumerable<IResponseDto>> GetAll<IResponseDto>();
        public Task<IResponseDto> GetById<IResponseDto>(int id);
    }
}
