using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Common;
using Storage.Iterfaces;

namespace WebAPI.Interfaces
{
    public interface ICrudService<T> where T : IDbModel
    {
        public Task<IReponseDto> Create<IReponseDto>(IRequestDto model);
        public Task<IEnumerable<IResponseDto>> GetAll<IResponseDto>();
        public Task<IResponseDto> GetById<IResponseDto>(int id);
        public Task Update(IRequestDto updateModel, int id);
        public Task Delete(int id);
    }
}
