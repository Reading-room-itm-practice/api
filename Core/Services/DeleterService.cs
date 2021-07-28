using Core.Interfaces;
using Storage.Iterfaces;
using Storage.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Services
{
    public class DeleterService<T> : IDeleterService<T> where T : AuditableModel, IDbMasterKey
    {
        private readonly IBaseRepository<T> _repository;
        private readonly IModifyAvalibilityChecker _modifyAvalibilityChecker;

        public DeleterService(IBaseRepository<T> repository, IModifyAvalibilityChecker modifyAvalibilityChecker)
        {
            _repository = repository;
            _modifyAvalibilityChecker = modifyAvalibilityChecker;
        }

        public async Task Delete(int id)
        {
            var model = await _repository.FindByConditions(x => x.Id == id);
            await _modifyAvalibilityChecker.CheckCanBeModify(model.FirstOrDefault());
            await _repository.Delete(model.FirstOrDefault());
        }
    }
}
