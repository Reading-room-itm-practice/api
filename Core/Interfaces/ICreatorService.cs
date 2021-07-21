using Core.Common;
using Storage.Iterfaces;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ICreatorService<T> where T : IDbModel
    {
        public Task<IReponseDto> Create<IReponseDto>(IRequest model);
    }
}
