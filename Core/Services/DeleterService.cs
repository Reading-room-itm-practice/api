using Core.Exceptions;
using Core.Interfaces;
using Storage.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Services
{
    public class DeleterService<T> : IDeleterService<T> where T : class, IDbMasterKey
    {
        private readonly IBaseRepository<T> _repository;

        public DeleterService(IBaseRepository<T> repository)
        {
            _repository = repository;
        }

        public async Task Delete(int id)
        {
            var model = await _repository.FindByConditions(x => x.Id == id);

            if (model.FirstOrDefault() == null)
            {
                throw new NotFoundException("Entity does not exists");
            }

            await _repository.Delete(model.FirstOrDefault());
        }
    }
}
