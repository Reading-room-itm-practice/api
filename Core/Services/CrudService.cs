using Core.Common;
using Core.Interfaces;
using Core.Response;
using Storage.Iterfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Services
{
    public class CrudService<T> : ICrudService<T> where T : class, IDbModel, IDbMasterKey
    {
        private readonly ICreatorService<T> _creator;
        private readonly IGetterService<T> _getter;
        private readonly IUpdaterService<T> _updater;
        private readonly IDeleterService<T> _deleter;

        public CrudService(ICreatorService<T> creator, IGetterService<T> getter, IUpdaterService<T> updater, IDeleterService<T> deleter)
        {
            _creator = creator;
            _getter = getter;
            _updater = updater;
            _deleter = deleter;
        }

        public async Task<IResponseDto> Create<IResponseDto>(IRequest requestDto)
        {
            return await _creator.Create<IResponseDto>(requestDto);
        }

        public async Task<PagedResponse<IEnumerable<IResponseDto>>> GetAll<IResponseDto>(PaginationFilter filter, string route)
        {
            return await _getter.GetAll<IResponseDto>(filter, route);
        }

        public async Task<IResponseDto> GetById<IResponseDto>(int id)
        {
            return await _getter.GetById<IResponseDto>(id);
        }

        public async Task Update(IRequest requestDto, int id)
        {
            await _updater.Update(requestDto, id);
        }

        public async Task Delete(int id)
        {
            await _deleter.Delete(id);
        }
    }
}
