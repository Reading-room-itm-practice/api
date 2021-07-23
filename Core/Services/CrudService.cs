using Core.Common;
using Core.Interfaces;
using Core.ServiceResponses;
using Storage.Iterfaces;
using Storage.Interfaces;
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

        public async Task<ServiceResponse<IDto>> Create<IDto>(IRequest requestDto)
        {
            return await _creator.Create<IDto>(requestDto);
        }

        public async Task<ServiceResponse<IEnumerable<IDto>>> GetAll<IDto>()
        {
            return await _getter.GetAll<IDto>();
        }

        public async Task<ServiceResponse<IDto>> GetById<IDto>(int id)
        {
            return await _getter.GetById<IDto>(id);
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
