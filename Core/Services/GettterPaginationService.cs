using AutoMapper;
using Core.Common;
using Core.Interfaces;
using Core.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Services
{
    public class GettterPaginationService : IGettterPaginationService
    {
        private readonly IPaginationRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public GettterPaginationService(IPaginationRepository postRepository, IMapper mapper, IUriService uriService)
        {
            _repository = postRepository;
            _mapper = mapper;
            _uriService = uriService;
        }

        async Task<ServiceResponse<PagedResponse<IEnumerable<T1>>>> IGettterPaginationService.GetAll<T, T1>(PaginationFilter filter, string route) where T : class
        {
            filter.Valid();
            var models = await _repository.FindAll<T>(filter);
            var data = _mapper.Map<IEnumerable<T1>>(models.Data);
            var pagedReponse = PaginationHelper.CreatePagedReponse(data, filter, models.Quantity, _uriService, route);

            return ServiceResponse<PagedResponse<IEnumerable<T1>>>.Success(pagedReponse, "Retrived resource");
        }

    }
}
