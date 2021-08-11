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
    public class ApprovedGetterService<T> : IApprovedGetterService<T> where T : class, IApproveable, IDbMasterKey
    {
        private readonly IBaseRepository<T> _repository;
        private readonly IMapper _mapper;
        public ApprovedGetterService(IBaseRepository<T> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _uriService = uriService;
        }

        public async Task<ServiceResponse<IEnumerable<IDto>>> GetAllApproved<IDto>()
        {
            var models = await _repository.FindByConditions(x => x.Approved);
            var responseModels = _mapper.Map<IEnumerable<IDto>>(models);

            return ServiceResponse<IEnumerable<IDto>>.Success(responseModels, "Retrieved list with resorces");
        }

        public async Task<ServiceResponse<IDto>> GetApprovedById<IDto>(int id)
        {
            var model = await _repository.FindByConditions(x => x.Approved && x.Id == id);
            var responseModel = _mapper.Map<IDto>(model.FirstOrDefault());

            return ServiceResponse<IDto>.Success(responseModel, "Retrieved resource");
        }
    }
}
