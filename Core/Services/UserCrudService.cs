using AutoMapper;
using Core.Common;
using Core.Interfaces;
using Core.Requests;
using Core.Response;
using Storage.Interfaces;
using Storage.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Core.Services
{
    public class UserCrudService<T> : CrudService<T>, IUserCrudService<T> where T : class, IApproveable, IDbMasterKey
    {
        private readonly IBaseRepository<T> _repository;
        private readonly IBaseRepository<Author> _authorRepository;
        private readonly IBaseRepository<Category> _categoryRepository;
        private readonly ICreatorService<T> _creator;
        private readonly IMapper _mapper;

        public UserCrudService(
            IBaseRepository<T> repository,
            IMapper mapper,
            ICreatorService<T> creator,
            IGetterService<T> getter,
            IUpdaterService<T> updater,
            IDeleterService<T> deleter,
            IBaseRepository<Author> booker,
            IBaseRepository<Category> categorer
            )
            : base(creator, getter, updater, deleter)
        {
            _repository = repository;
            _authorRepository = booker;
            _categoryRepository = categorer;
            _mapper = mapper;
            _creator = creator;
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

        public new async Task<ServiceResponse> Create<IDto>(IRequest requestDto)
        {
            if(requestDto.GetType() == typeof(BookRequest))
            {
                var model = _mapper.Map<Book>(requestDto);

                var author = await _authorRepository.FindByConditions(a => a.Id == model.AuthorId && a.Approved);
                var category = await _categoryRepository.FindByConditions(c => c.Id == model.CategoryId && c.Approved);
                if (!author.Any() || !category.Any())
                {
                    var responseModel = _mapper.Map<IDto>(model);
                    return ServiceResponse.Error( "Invalid author or category", HttpStatusCode.UnprocessableEntity);
                }   
            }
                
            return await _creator.Create<IDto>(requestDto);
        }
    }
}
