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
        protected readonly IBaseRepository<T> Repository;
        protected readonly IMapper Mapper;

        public GetterService(IBaseRepository<T> repository, IMapper mapper)
        {
            Repository = repository;
            Mapper = mapper;
        }

        public virtual async Task<ServiceResponse<IEnumerable<IDto>>> GetAll<IDto>()
        {
            var models = await Repository.FindAll();
            var responseModels =  Mapper.Map<IEnumerable<IDto>>(models);

            return ServiceResponse<IEnumerable<IDto>>.Success(responseModels, "Retrived list with resorces");
        }

        public virtual async Task<ServiceResponse<IDto>> GetById<IDto>(int id)
        {
            var model = await Repository.FindByConditions(x => x.Id == id);
            var responseModel = Mapper.Map<IDto>(model.FirstOrDefault());

            return ServiceResponse<IDto>.Success(responseModel, "Retrived resource");
        }
    }
}
