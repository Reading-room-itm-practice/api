using AutoMapper;
using Core.Interfaces;
using Core.ServiceResponses;
using Storage.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Services
{
    public class UserCrudService<T> : CrudService<T>, IUserCrudService<T> where T : class, IApproveable, IDbMasterKey<int>
    {
        private readonly IBaseRepository<T> _repository;
        private readonly IMapper _mapper;

        public UserCrudService(
            IBaseRepository<T> repository,
            IMapper mapper,
            ICreatorService<T> creator,
            IGetterService<T> getter,
            IUpdaterService<T> updater,
            IDeleterService<T> deleter)
            : base(creator, getter, updater, deleter)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public override async Task<ServiceResponse<IEnumerable<IDto>>> GetAll<IDto>()
        {
            var models = await _repository.FindByConditions(x => x.Approved);
            var responseModels = _mapper.Map<IEnumerable<IDto>>(models);

            return ServiceResponse<IEnumerable<IDto>>.Success(responseModels, "Retrieved list with resources");
        }

        public override async Task<ServiceResponse<IDto>> GetById<IDto>(int id)
        {
            var model = await _repository.FindByConditions(x => x.Approved && x.Id == id);
            var responseModel = _mapper.Map<IDto>(model.FirstOrDefault());

            return ServiceResponse<IDto>.Success(responseModel, "Retrieved resource");
        }
    }
}
