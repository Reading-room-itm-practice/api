using Core.Common;
using Core.Response;
using Storage.Iterfaces;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ICreatorService<T> where T : IDbModel
    {
        public Task<ServiceResponse<IDto>> Create<IDto>(IRequest model);
    }
}
