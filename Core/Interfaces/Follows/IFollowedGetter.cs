using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.ServiceResponses;
using Storage.Models.Follows;

namespace Core.Interfaces.Follows
{
    public interface IFollowedGetter<T> where T : Follow
    {
        public Task<ServiceResponse<IEnumerable<IDto>>> GetFollowed<IDto>(Guid followableId);
    }
}
