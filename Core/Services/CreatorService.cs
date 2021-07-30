using AutoMapper;
using Core.Common;
using Core.Interfaces;
using Core.ServiceResponses;
using Storage.Interfaces;
using System.Net;
using System.Threading.Tasks;

namespace Core.Services
{
    public class CreatorService<T> : ICreatorService<T> where T : class, IDbModel
    {
        private readonly IBaseRepository<T> _repository;
        private readonly IMapper _mapper;

        public CreatorService(IBaseRepository<T> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<IDto>> Create<IDto>(IRequest requestDto)
        {
            var model = _mapper.Map<T>(requestDto);
            await _repository.Create(model);
            var responseModel = _mapper.Map<IDto>(model);

            return ServiceResponse<IDto>.Success(responseModel, "Resorce has been created.", HttpStatusCode.Created);
        }
    }
}
