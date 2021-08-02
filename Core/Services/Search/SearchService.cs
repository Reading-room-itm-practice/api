using AutoMapper;
using Core.Common;
using Core.DTOs;
using Core.Enums;
using Core.Interfaces;
using Core.Interfaces.Search;
using Core.Response;
using System.Collections.Generic;

namespace Core.Services.Search
{
    public class SearchService : ISearchService
    {
        private readonly ISearchRepository _searchRepository;
        private readonly IUriService _uriService;
        private readonly IMapper _mapper;
        public SearchService(ISearchRepository searchRepository, IUriService uriService, IMapper mapper)
        {
            _searchRepository = searchRepository;
            _uriService = uriService;
            _mapper = mapper;
        }

        public ServiceResponse SearchEntity<Data, Dto>(PaginationFilter filter, string route, string searchString, SortType? sort, int? minYear = null, int? maxYear = null,
            int? categoryId = null, int? authorId = null) where Data : class where Dto : class , IDto
        {
            filter.Valid();
            var entities = _searchRepository.GetEntities<Data>(filter, searchString, sort);
            return typeof(SearchAll) != typeof(Data) ? EnumerableResponse<Data, Dto>(entities, filter, route) : Response<Data, Dto>(entities, filter, route);
        }

        private ServiceResponse Response<Data, Dto>(DataDto entities, PaginationFilter filter, string route)
        {
            var entitiesDto = _mapper.Map<DataDto<Dto>>(entities);
            var pagedReponse = PaginationHelper.CreatePagedReponse(entitiesDto.Data, filter, entitiesDto.Quantity, _uriService, route);
            var message = entitiesDto.Quantity == 0 ? $"No {typeof(Data).Name} search results found." : $"{typeof(Data).Name} search results retrieved.";

            return ServiceResponse<PagedResponse<Dto>>.Success(pagedReponse, message);
        }

        private ServiceResponse EnumerableResponse<Data, Dto>(DataDto entities, PaginationFilter filter, string route)
        {
            var entitiesDto = _mapper.Map<DataDto<IEnumerable<Dto>>>(entities);
            var pagedReponse = PaginationHelper.CreatePagedReponse(entitiesDto.Data, filter, entitiesDto.Quantity, _uriService, route);
            var message = entitiesDto.Quantity == 0 ? $"No {typeof(Data).Name} search results found." : $"{typeof(Data).Name} search results retrieved.";

            return ServiceResponse<PagedResponse<IEnumerable<Dto>>>.Success(pagedReponse, message);
        }
    }
}
