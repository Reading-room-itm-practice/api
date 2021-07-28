using Core.Requests.Follows;
using Core.ServiceResponses;
using Storage.Models.Follows;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IFollowCreatorService<T> where T : Follow
    {
        public Task<ServiceResponse> Create(FollowRequest followRequest);
    }
}
