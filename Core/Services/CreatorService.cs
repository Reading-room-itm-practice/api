using AutoMapper;
using Core.Common;
using Core.Interfaces;
using Storage.Iterfaces;
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

        public async Task<IResponseDto> Create<IResponseDto>(IRequest requestDto)
        {
            var model = _mapper.Map<T>(requestDto);
            await _repository.Create(model);

            return _mapper.Map<IResponseDto>(model);
        }
    }
}
