using AutoMapper;
using Core.Common;
using Core.Interfaces;
using Core.Requests;
using Core.Response;
using Storage.Interfaces;
using Storage.Models;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Core.Services
{
    public class CreatorService<T> : ICreatorService<T> where T : class, IDbModel
    {
        private readonly IBaseRepository<T> _repository;
        private readonly IBaseRepository<Author> _authorRepository;
        private readonly IBaseRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;

        public CreatorService(IBaseRepository<T> repository, IBaseRepository<Author> booker, IBaseRepository<Category> categorer, IMapper mapper)
        {
            _repository = repository;
            _authorRepository = booker;
            _categoryRepository = categorer;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<IDto>> Create<IDto>(IRequest requestDto)
        {
            if (requestDto.GetType() == typeof(BookRequest) || requestDto.GetType() == typeof(ApproveBookRequest))
            {
                var bookModel = _mapper.Map<Book>(requestDto);

                var author = await _authorRepository.FindByConditions(a => a.Id == bookModel.AuthorId && a.Approved);
                var category = await _categoryRepository.FindByConditions(c => c.Id == bookModel.CategoryId && c.Approved);
                if (!author.Any() || !category.Any())
                {
                    var responseBookModel = _mapper.Map<IDto>(bookModel);
                    return ServiceResponse<IDto>.Error(responseBookModel, "Invalid author or category", HttpStatusCode.UnprocessableEntity);
                }
            }
            var model = _mapper.Map<T>(requestDto);
            await _repository.Create(model);
            var responseModel = _mapper.Map<IDto>(model);

            return ServiceResponse<IDto>.Success(responseModel, "Resorce has been created.", HttpStatusCode.Created);
        }
    }
}
