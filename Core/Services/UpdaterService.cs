using AutoMapper;
using Core.Common;
using Core.Exceptions;
using Core.Interfaces;
using Storage.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Services
{
    public class UpdaterService<T> : IUpdaterService<T> where T : class, IDbModel, IDbMasterKey
    {
        private readonly IBaseRepository<T> _repository;
        private readonly IMapper _mapper;

        public UpdaterService(IBaseRepository<T> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Update(IRequest requestDto, int id)
        {
            var model = await _repository.FindByConditions(x => x.Id == id);

            if (model.FirstOrDefault() == null)
            {
                throw new NotFoundException("Entity does not exists");
            }

            var updatedModel = _mapper.Map(requestDto, model.FirstOrDefault());
            await _repository.Edit(updatedModel);
        }
    }
}
