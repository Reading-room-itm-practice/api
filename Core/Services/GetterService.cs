using AutoMapper;
using Core.Common;
using Core.Interfaces;
using Core.Response;
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
        private readonly IUriService _uriService;

        public GetterService(IBaseRepository<T> postRepository, IMapper mapper, IUriService uriService)
        {
            _repository = postRepository;
            _mapper = mapper;
            _uriService = uriService;
        }

        async Task<ServiceResponse<PagedResponse<IEnumerable<IDto>>>> IGetterService<T>.GetAll<IDto>(PaginationFilter filter, string route)
        {
            var models = await _repository.FindAll(filter);
            var data = _mapper.Map<IEnumerable<IDto>>(models.Data);
            var pagedReponse = PaginationHelper.CreatePagedReponse(data, filter, models.Quantity, _uriService, route);

            return ServiceResponse<PagedResponse<IEnumerable<IDto>>>.Success(pagedReponse, "Retrived resource");
        }

        public async Task<ServiceResponse<IDto>> GetById<IDto>(int id)
        {
            var model = await _repository.FindByConditions(x => x.Id == id);
            var responseModel = _mapper.Map<IDto>(model.FirstOrDefault());

            return ServiceResponse<IDto>.Success(responseModel, "Retrived resource");
        }

    }
}
