using AutoMapper;
using Core.Interfaces;
using Core.ServiceResponses;
using Storage.Interfaces;
using Storage.Models;
using Storage.Models.Follows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Services
{
    public sealed class FollowersGetter<T> : IFollowersGetter<T> where T : AuditableModel, IFollowable<Follow>, IDbMasterKey<int>
    {
        private readonly IBaseRepository<T> _repository;
        private readonly IMapper _mapper;
        public FollowersGetter(IBaseRepository<T> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<IEnumerable<IDto>>> GetFollowers<IDto>(int followableId)
        {
            var models = await _repository.FindByConditions(i=>i.Id == followableId);
            var followers = models.FirstOrDefault()?.Followers;
            var responseModels = _mapper.Map<IEnumerable<IDto>>(followers);

            return ServiceResponse<IEnumerable<IDto>>.Success(responseModels, "Retrieved list with resources");
        }

        public async Task<ServiceResponse<IEnumerable<IDto>>> GetFollowers<IDto>(Guid followableId)
        {
            var models = await _repository.FindByConditions(i => i.Id == followableId);
            var followers = models.FirstOrDefault()?.Followers;
            var responseModels = _mapper.Map<IEnumerable<IDto>>(followers);

            return ServiceResponse<IEnumerable<IDto>>.Success(responseModels, "Retrieved list with resources");
        }

    }
}
