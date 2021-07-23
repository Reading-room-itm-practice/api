using Core.Common;
using Storage.Interfaces;
using Core.ServiceResponses;
using Storage.Iterfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ICrudService<T> : ICreatorService<T>, IGetterService<T>, IUpdaterService<T>, IDeleterService<T> where T : IDbModel
    {
        public new Task<ServiceResponse<IDto>> Create<IDto>(IRequest model);
        public new Task<ServiceResponse<IEnumerable<IDto>>> GetAll<IDto>();
        public new Task<ServiceResponse<IDto>> GetById<IDto>(int id);
        public new Task Update(IRequest updateModel, int id);
        public new Task Delete(int id);
    }
}
