using Core.Interfaces;
using Storage.Interfaces;
using Storage.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Services
{
    public class DeleterService<T> : IDeleterService<T> where T : AuditableModel, IDbMasterKey<int>
    {
        protected readonly IBaseRepository<T> Repository;
        protected readonly IModifyAvalibilityChecker ModifyAvalibilityChecker;

        public DeleterService(IBaseRepository<T> repository, IModifyAvalibilityChecker modifyAvalibilityChecker)
        {
            Repository = repository;
            ModifyAvalibilityChecker = modifyAvalibilityChecker;
        }

        public virtual async Task Delete(int id)
        {
            var model = await Repository.FindByConditions(x => x.Id == id);
            await ModifyAvalibilityChecker.CheckCanBeModify(model.FirstOrDefault());
            await Repository.Delete(model.FirstOrDefault());
        }
    }
}
