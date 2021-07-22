using Core.Common;
using Core.Services;
using Storage.Iterfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ICrudService<T> : ICreatorService<T>, IGetterService<T>, IUpdaterService<T>, IDeleterService<T> where T : IDbModel
    {
        public Task<IReponseDto> Create<IReponseDto>(IRequest model);
        public Task<IEnumerable<IResponseDto>> GetAll<IResponseDto>(PaginationFilter filter);
        public Task<IResponseDto> GetById<IResponseDto>(int id);
        public Task Update(IRequest updateModel, int id);
        public Task Delete(int id);
    }
}
