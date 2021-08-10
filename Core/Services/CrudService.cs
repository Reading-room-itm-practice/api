using Core.Common;
using Core.Interfaces;
using Core.ServiceResponses;
using Storage.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Services
{
    public class CrudService<T> : ICrudService<T> where T : class, IDbMasterKey<int>
    {
        protected readonly ICreatorService<T> Creator;
        protected readonly IGetterService<T> Getter;
        protected readonly IUpdaterService<T> Updater;
        protected readonly IDeleterService<T> Deleter;

        public CrudService(ICreatorService<T> creator, IGetterService<T> getter, IUpdaterService<T> updater, IDeleterService<T> deleter)
        {
            Creator = creator;
            Getter = getter;
            Updater = updater;
            Deleter = deleter;
        }

        public virtual async Task<ServiceResponse<IDto>> Create<IDto>(IRequest requestDto)
        {
            return await Creator.Create<IDto>(requestDto);
        }

        public virtual async Task<ServiceResponse<IEnumerable<IDto>>> GetAll<IDto>()
        {
            return await Getter.GetAll<IDto>();
        }

        public virtual async Task<ServiceResponse<IDto>> GetById<IDto>(int id)
        {
            return await Getter.GetById<IDto>(id);
        }

        public virtual async Task Update(IRequest requestDto, int id)
        {
            await Updater.Update(requestDto, id);
        }

        public virtual async Task Delete(int Id)
        {
            await Deleter.Delete(Id);
        }
    }
}
