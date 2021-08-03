using AutoMapper;
using Core.Common;
using Core.Interfaces;
using Storage.Models;
using Storage.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Services
{
    public class UpdaterService<T> :  IUpdaterService<T> where T : AuditableModel, IDbMasterKey<int>
    {
        protected readonly IBaseRepository<T> Repository;
        protected readonly IModifyAvalibilityChecker ModifyAvalibilityChecker;
        protected readonly IMapper Mapper;

        public UpdaterService(IBaseRepository<T> repository, IModifyAvalibilityChecker modifyAvalibilityChecker, IMapper mapper)
        {
            Repository = repository;
            ModifyAvalibilityChecker = modifyAvalibilityChecker;
            Mapper = mapper;
        }

        public virtual async Task Update(IRequest requestDto, int id)
        {
            var model = await Repository.FindByConditions(x => x.Id == id);
            await ModifyAvalibilityChecker.CheckCanBeModify(model.FirstOrDefault());
            var updatedModel = Mapper.Map(requestDto, model.FirstOrDefault());
            await Repository.Edit(updatedModel);
        }
    }
}
