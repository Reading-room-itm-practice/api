using AutoMapper;
using Core.Interfaces;
using Core.ServiceResponses;
using Storage.Iterfaces;
using Storage.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Services
{
    public class ExtendedGetterService<T> : GetterService<T>, IExtendedGetterService<T> where T : AuditableModel, IDbMasterKey
    {
        public ExtendedGetterService(IBaseRepository<T> repository, IMapper mapper) : base(repository, mapper)
        {
        }

        public async Task<ServiceResponse<IEnumerable<IDto>>> GetAllByCreator<IDto>(Guid userId)
        {
            var models = await _repository.FindByConditions(x => x.CreatorId == userId);
            var responseModels = _mapper.Map<IEnumerable<IDto>>(models);

            return ServiceResponse<IEnumerable<IDto>>.Success(responseModels, "Retrived list with resorces by creator id");
        }
    }
}
