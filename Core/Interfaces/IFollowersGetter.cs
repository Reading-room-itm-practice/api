using Core.ServiceResponses;
using Storage.Interfaces;
using Storage.Models.Follows;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IFollowersGetter<T> where T : IFollowable<Follow>, IDbMasterKey
    {
        public Task<ServiceResponse<IEnumerable<IDto>>> GetFollowers<IDto>(int followableId);
        public Task<ServiceResponse<IEnumerable<IDto>>> GetFollowers<IDto>(Guid followableId);
    }
}
