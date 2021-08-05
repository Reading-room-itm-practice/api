using Core.DTOs.Follows;
using Core.Interfaces;
using Core.Requests.Follows;
using Core.ServiceResponses;
using Microsoft.AspNetCore.Mvc;
using Storage.Models.Follows;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Threading.Tasks;
using Core.Interfaces.Follows;

namespace WebAPI.Controllers.Follows
{
    [ApiController]
    [Route("api")]
    public class UserFollowsController : ControllerBase
    {
        private readonly IDeleterService<UserFollow> _deleterService;
        private readonly IFollowedGetter<UserFollow> _followedGetter;
        private readonly ICreatorService<UserFollow> _creatorService;
        private readonly IFollowersGetter _followersGetter; 

        public UserFollowsController(IDeleterService<UserFollow> deleter,
            IFollowedGetter<UserFollow> followedGetter,
            ICreatorService<UserFollow> creator,
            IFollowersGetter followersGetter)
        {
            _deleterService = deleter;
            _followedGetter = followedGetter;
            _creatorService = creator;
            _followersGetter = followersGetter;
        }

        [SwaggerOperation(Summary = "Retrieves all categories follows")]
        [Route("users/{id:guid}/user-follows")]
        [HttpGet]
        public async Task<ServiceResponse> FollowedIndex(Guid id)
        {
            return await _followedGetter.GetFollowed<UserFollowDto>(id);
        }

        [SwaggerOperation(Description = "Retrieves all authors follows")]
        [Route("users/{id:guid}/followers")]
        [HttpGet]
        public async Task<ServiceResponse> FollowersIndex(Guid id)
        {
            return await _followersGetter.GetUserFollowers<FollowerDto>(id);
        }

        [SwaggerOperation(Summary = "Create category follow for logged user")]
        [Route("users/{id:guid}/follows")]
        [HttpPost]
        public async Task<ServiceResponse> Create(Guid id)
        {
            return await _creatorService.Create<UserFollowDto>(new UserFollowRequest { FollowableId = id });
        }

        [SwaggerOperation(Summary = "Delete a category follow by unique id")]
        [Route("user-follows/{id:int}")]
        [HttpDelete]
        public async Task<ServiceResponse> Delete(int id)
        {
            await _deleterService.Delete(id);

            return ServiceResponse.Success("Resource has been deleted");
        }
    }
}
