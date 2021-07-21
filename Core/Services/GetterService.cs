using AutoMapper;
using Core.Interfaces;
using Storage.Iterfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Services
{
    public class GetterService<T> : IGetterService<T> where T : class, IDbModel, IDbMasterKey
    {
        private readonly IBaseRepository<T> _repository;
        private readonly IMapper _mapper;

        public GetterService(IBaseRepository<T> postRepository, IMapper mapper)
        {
            _repository = postRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<IResponseDto>> GetAll<IResponseDto>()
        {
            var models = await _repository.FindAll();

            return _mapper.Map<IEnumerable<IResponseDto>>(models);
        }

        public async Task<IResponseDto> GetById<IResponseDto>(int id)
        {
            var model = await _repository.FindByConditions(x => x.Id == id);

            return _mapper.Map<IResponseDto>(model.FirstOrDefault());
        }
    }
}
