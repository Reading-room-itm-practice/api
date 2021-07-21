using Core.Common;
using Storage.Iterfaces;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IUpdaterService<T> where T : IDbModel
    {
        public Task Update(IRequest updateModel, int id);
    }
}
