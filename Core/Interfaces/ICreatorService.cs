using Core.Common;
using Storage.Interfaces;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ICreatorService<T> where T : IDbModel
    {
        public Task<ServiceResponse<IDto>> Create<IDto>(IRequest model);
    }
}
