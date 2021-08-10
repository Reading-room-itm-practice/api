using Core.Common;
using Core.Response;
using Storage.Interfaces;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IStandardCrudService<T> : ICrudService<T> where T : class, IApproveable, IDbMasterKey
    {
        public new Task<ServiceResponse> Create<IDto>(IRequest requestDto);
    }
}
