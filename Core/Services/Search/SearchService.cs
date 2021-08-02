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

        public ServiceResponse SearchEntity<InData, OutData>(PaginationFilter filter, string route, string searchString, SortType? sort, int? minYear = null, int? maxYear = null,
            int? categoryId = null, int? authorId = null) where InData : class where OutData : class , IDto
        {
            filter.Valid();
            var entities = _searchRepository.GetEntities<InData>(filter, searchString, sort);
            var entitiesDto = _mapper.Map<DataDto<IEnumerable<OutData>>>(entities);
            var pagedReponse = PaginationHelper.CreatePagedReponse(entitiesDto.Data, filter, entitiesDto.Quantity, _uriService, route);
            var message = entitiesDto.Quantity == 0 ? $"No {typeof(InData).Name} search results found." : $"{typeof(InData).Name} search results retrieved.";

            return ServiceResponse<PagedResponse<IEnumerable<OutData>>>.Success(pagedReponse, message);
        }
        
    }
}
