using Core.ServiceResponses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces.Follows
{
    public interface IFollowersGetter
    {
        public Task<ServiceResponse<IEnumerable<IDto>>> GetUserFollowers<IDto>(Guid followableId);
        public Task<ServiceResponse<IEnumerable<IDto>>> GetAuthorFollowers<IDto>(int followableId);
        public Task<ServiceResponse<IEnumerable<IDto>>> GetCategoryFollowers<IDto>(int followableId);
    }
}
