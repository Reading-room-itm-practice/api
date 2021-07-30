using AutoMapper;
using Core.Common;
using Core.Interfaces;
using Storage.Models;
using Storage.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Services
{
    public class UpdaterService<T> :  IUpdaterService<T> where T : AuditableModel, IDbMasterKey
    {
        private readonly IBaseRepository<T> _repository;
        private readonly IModifyAvalibilityChecker _modifyAvalibilityChecker;
        private readonly IMapper _mapper;

        public UpdaterService(IBaseRepository<T> repository, IModifyAvalibilityChecker modifyAvalibilityChecker, IMapper mapper)
        {
            _repository = repository;
            _modifyAvalibilityChecker = modifyAvalibilityChecker;
            _mapper = mapper;
        }

        public async Task Update(IRequest requestDto, int id)
        {
            var model = await _repository.FindByConditions(x => x.Id == id);
            await _modifyAvalibilityChecker.CheckCanBeModify(model.FirstOrDefault());
            var updatedModel = _mapper.Map(requestDto, model.FirstOrDefault());
            await _repository.Edit(updatedModel);
        }
    }
}
