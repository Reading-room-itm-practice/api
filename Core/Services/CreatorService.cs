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
        protected readonly IBaseRepository<T> Repository;
        protected readonly IMapper Mapper;

        public CreatorService(IBaseRepository<T> repository, IMapper mapper)
        {
            Repository = repository;
            Mapper = mapper;
        }

        public virtual async Task<ServiceResponse<IDto>> Create<IDto>(IRequest requestDto)
        {
            var model = Mapper.Map<T>(requestDto);
            await Repository.Create(model);
            var responseModel = Mapper.Map<IDto>(model);

            return ServiceResponse<IDto>.Success(responseModel, "Resource has been created.", HttpStatusCode.Created);
        }
    }
}
