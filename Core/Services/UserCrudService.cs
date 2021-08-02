﻿using AutoMapper;
using Core.Common;
using Core.Interfaces;
using Core.Response;
using Storage.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Services
{
    public class UserCrudService<T> : CrudService<T>, IUserCrudService<T> where T : class, IApproveable, IDbMasterKey
    {
        private readonly IBaseRepository<T> _repository;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public UserCrudService(
            IBaseRepository<T> repository,
            IMapper mapper,
            IUriService uriService,
            ICreatorService<T> creator,
            IGetterService<T> getter,
            IUpdaterService<T> updater,
            IDeleterService<T> deleter)
            : base(creator, getter, updater, deleter)
        {
            _repository = repository;
            _mapper = mapper;
            _uriService = uriService;
        }

        public override async Task<ServiceResponse<IEnumerable<IDto>>> GetAll<IDto>()
        {
            var models = await _repository.FindByConditions(x => x.Approved);
            var responseModels = _mapper.Map<IEnumerable<IDto>>(models);

            return ServiceResponse<IEnumerable<IDto>>.Success(responseModels, "Retrieved list with resorces");
        }

        public override async Task<ServiceResponse<IDto>> GetById<IDto>(int id)
        {
            var model = await _repository.FindByConditions(x => x.Approved && x.Id == id);
            var responseModel = _mapper.Map<IDto>(model.FirstOrDefault());

            return ServiceResponse<IDto>.Success(responseModel, "Retrieved resource");
        }
    }
}
