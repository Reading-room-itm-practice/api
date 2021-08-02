using AutoMapper;
using Core.Common;
using Core.Interfaces;
using Core.Response;
using Storage.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Services
{
    public class GetterService<T> : IGetterService<T> where T : class, IDbMasterKey
    {
        private readonly IBaseRepository<T> _repository;
        private readonly IMapper _mapper;

        public GetterService(IBaseRepository<T> postRepository, IMapper mapper)
        {
            _repository = postRepository;
            _mapper = mapper;
        }

        async Task<ServiceResponse<IEnumerable<IDto>>> IGetterService<T>.GetAll<IDto>()
        {

            var models = await _repository.FindAll();
            var data = _mapper.Map<IEnumerable<IDto>>(models);

            return ServiceResponse<IEnumerable<IDto>>.Success(data, "Retrived resource");
        }

        public async Task<ServiceResponse<IDto>> GetById<IDto>(int id)
        {
            var model = await _repository.FindByConditions(x => x.Id == id);
            var responseModel = _mapper.Map<IDto>(model.FirstOrDefault());

            return ServiceResponse<IDto>.Success(responseModel, "Retrived resource");
        }

    }
}
