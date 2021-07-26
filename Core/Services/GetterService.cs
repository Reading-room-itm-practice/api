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

        public async Task<PagedResponse<IResponseDto>> GetAll<IResponseDto>(PaginationFilter filter, string route)
        {
            var models = await _repository.FindAll(filter);
            var data = _mapper.Map<IEnumerable<IResponseDto>>(models.data);
            var pagedReponse = PaginationHelper.CreatePagedReponse(data, filter, models.count, _uriService, route);

            return pagedReponse;
        }

        public async Task<IResponseDto> GetById<IResponseDto>(int id)
        {
            var model = await _repository.FindByConditions(x => x.Id == id);

            return _mapper.Map<IResponseDto>(model.FirstOrDefault());
        }
    }
}
