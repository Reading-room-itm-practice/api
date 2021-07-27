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
    public class UserBookCrudService : CrudService<Book>, IUserBookCrudService
    {
        private readonly IBaseRepository<Book> _repository;
        private readonly IMapper _mapper;

        public UserBookCrudService(
            IBaseRepository<Book> repository,
            IMapper mapper,
            ICreatorService<Book> creator,
            IGetterService<Book> getter,
            IUpdaterService<Book> updater,
            IDeleterService<Book> deleter)
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
