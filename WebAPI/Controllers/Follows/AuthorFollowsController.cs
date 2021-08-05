using Core.DTOs.Follows;
using Core.Interfaces;
using Core.Requests.Follows;
using Core.ServiceResponses;
using Microsoft.AspNetCore.Mvc;
using Storage.Models.Follows;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Core.Interfaces.Follows;

namespace WebAPI.Controllers.Follows
{
    [ApiController]
    [Route("api")]
    public class AuthorFollowsController : ControllerBase
    {
        private readonly IDeleterService<AuthorFollow> _deleter;
        private readonly IFollowedGetter<AuthorFollow> _followedGetter;
        private readonly ICreatorService<AuthorFollow> _creator;
        private readonly IFollowersGetter _followersGetter;

        public AuthorFollowsController(IDeleterService<AuthorFollow> deleter,
            IFollowedGetter<AuthorFollow> followedGetter,
            ICreatorService<AuthorFollow> creator,
            IFollowersGetter followersGetter)
        {
            _deleter = deleter;
            _followedGetter = followedGetter;
            _creator = creator;
            _followersGetter = followersGetter;
        }

        [SwaggerOperation(Description = "Retrieves all authors follows")]
        [Route("users/{id:guid}/author-follows")]
        [HttpGet]
        public async Task<ServiceResponse> FollowedIndex(Guid id)
        {
            return await _followedGetter.GetFollowed<FollowDto>(id);
        }

        [SwaggerOperation(Description = "Retrieves all authors follows")]
        [Route("authors/{id:int}/followers")]
        [HttpGet]
        public async Task<ServiceResponse> FollowersIndex(int id)
        {
            return await _followersGetter.GetAuthorFollowers<FollowerDto>(id);
        }

        [SwaggerOperation(Description  = "Create author follow for logged user")]
        [Route("authors/{id:int}/follows")]
        [HttpPost]
        public async Task<ServiceResponse> Create(int id)
        {
            return await _creator.Create<FollowDto>(
                new FollowRequest {FollowableId = id , CreatorId = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier)) });
        }

        [SwaggerOperation(Description = "Delete a follow by unique id")]
        [Route("author-follows/{id:int}")]
        [HttpDelete]
        public async Task<ServiceResponse> Delete(int id)
        {
            await _deleter.Delete(id);

            return ServiceResponse.Success("Resource has been deleted");
        }
    }
}
