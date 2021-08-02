using AutoMapper;
using Core.Interfaces;
using Core.ServiceResponses;
using Storage.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Services
{
    public class GetterService<T> : IGetterService<T> where T : class, IDbMasterKey<int>
    {
        protected readonly IBaseRepository<T> _repository;
        protected readonly IMapper _mapper;

        public GetterService(IBaseRepository<T> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public virtual async Task<ServiceResponse<IEnumerable<IDto>>> GetAll<IDto>()
        {
            var models = await _repository.FindAll();
            var responseModels =  _mapper.Map<IEnumerable<IDto>>(models);

            return ServiceResponse<IEnumerable<IDto>>.Success(responseModels, "Retrived list with resorces");
        }

        public virtual async Task<ServiceResponse<IDto>> GetById<IDto>(int id)
        {
            var model = await _repository.FindByConditions(x => x.Id == id);
            var responseModel = _mapper.Map<IDto>(model.FirstOrDefault());

            return ServiceResponse<IDto>.Success(responseModel, "Retrived resource");
        }
    }
}
