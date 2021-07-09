using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Storage.Iterfaces;

namespace Core.Interfaces
{
    public interface IGetterService<T> where T : IDbModel
    {
        public Task<IEnumerable<IResponseDto>> GetAll<IResponseDto>();
        public Task<IResponseDto> GetById<IResponseDto>(int id);
    }
}
