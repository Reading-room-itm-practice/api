using AutoMapper;
using Core.Interfaces;
using Core.ServiceResponses;
using Storage.Interfaces;
using Storage.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Services
{
    public class GetterByCreatorService<T> : GetterService<T>, IGetterByCreatorService<T> where T : AuditableModel, IDbMasterKey
    {
        public GetterByCreatorService(IBaseRepository<T> repository, IMapper mapper) : base(repository, mapper)
        {
        }

        public async Task<ServiceResponse<IEnumerable<IDto>>> GetAllByCreator<IDto>(Guid userId)
        {
            var models = await _repository.FindByConditions(x => x.CreatorId == userId);
            var responseModels = _mapper.Map<IEnumerable<IDto>>(models);

            return ServiceResponse<IEnumerable<IDto>>.Success(responseModels, "Retrieved list with resources by creator id");
        }
    }
}
