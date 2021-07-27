using AutoMapper;
using Core.Interfaces;
using Core.ServiceResponses;
using Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class UserCategoryCrudService : CrudService<Category>, IUserCategoryCrudService
    {
        private readonly IBaseRepository<Category> _repository;
        private readonly IMapper _mapper;

        public UserCategoryCrudService(
            IBaseRepository<Category> repository,
            IMapper mapper,
            ICreatorService<Category> creator,
            IGetterService<Category> getter,
            IUpdaterService<Category> updater,
            IDeleterService<Category> deleter)
            : base(creator, getter, updater, deleter)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public override async Task<ServiceResponse<IEnumerable<IDto>>> GetAll<IDto>()
        {
            var models = await _repository.FindByConditions(x => x.Approved == true);
            var responseModels = _mapper.Map<IEnumerable<IDto>>(models);

            return ServiceResponse<IEnumerable<IDto>>.Success(responseModels, "Retrived list with resorces");
        }

        public override async Task<ServiceResponse<IDto>> GetById<IDto>(int id)
        {
            var model = await _repository.FindByConditions(x => x.Approved == true && x.Id == id);
            var responseModel = _mapper.Map<IDto>(model.FirstOrDefault());

            return ServiceResponse<IDto>.Success(responseModel, "Retrived resource");
        }
    }
}
