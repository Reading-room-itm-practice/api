using Storage.Iterfaces;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IDeleterService<T> where T : IDbModel
    {
        public Task Delete(int id);
    }
}
