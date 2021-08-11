using Core.Common;
using Core.Interfaces;
using Core.Response;
using Storage.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Services
{
    public class CrudService<T> : ICrudService<T> where T : class, IDbMasterKey
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

        public virtual async Task<ServiceResponse<IDto>> Create<IDto>(IRequest requestDto)
        {
            return await _creator.Create<IDto>(requestDto);
        }

        public virtual async Task<ServiceResponse<IEnumerable<IDto>>> GetAll<IDto>()
        {
            return await _getter.GetAll<IDto>();
        }

        public virtual async Task<ServiceResponse<IDto>> GetById<IDto>(int id)
        {
            return await _getter.GetById<IDto>(id);
        }

        public virtual async Task Update(IRequest requestDto, int id)
        {
            await _updater.Update(requestDto, id);
        }

        public virtual async Task Delete(int id)
        {
            await _deleter.Delete(id);
        }
    }
}
