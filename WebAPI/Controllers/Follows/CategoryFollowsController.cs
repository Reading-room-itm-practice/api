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
    public class CategoryFollowsController : ControllerBase
    {
        private readonly IDeleterService<CategoryFollow> _deleterService;
        private readonly IFollowedGetter<CategoryFollow> _followedGetter;
        private readonly ICreatorService<CategoryFollow> _creatorService;
        private readonly IFollowersGetter _followersGetter;

        public CategoryFollowsController(IDeleterService<CategoryFollow> deleter, 
            IFollowedGetter<CategoryFollow> followedGetter,
            ICreatorService<CategoryFollow> creator,
            IFollowersGetter followersGetter)
        {
            _deleterService = deleter;
            _followedGetter = followedGetter;
            _creatorService = creator;
            _followersGetter = followersGetter;
        }

        [SwaggerOperation(Summary = "Retrieves all categories follows")]
        [Route("api/users/{id:guid}/category-follows")]
        [HttpGet]
        public async Task<ServiceResponse> FollowedIndex(Guid id)
        {
            return await _followedGetter.GetFollowed<FollowDto>(id);
        }

        [SwaggerOperation(Description = "Retrieves all authors follows")]
        [Route("categories/{id:int}/followers")]
        [HttpGet]
        public async Task<ServiceResponse> FollowersIndex(int id)
        {
            return await _followersGetter.GetCategoryFollowers<FollowerDto>(id);
        }

        [SwaggerOperation(Summary = "Create category follow for logged user")]
        [Route("api/categories/{id:int}/follows")]
        [HttpPost]
        public async Task<ServiceResponse> Create(int id)
        {
            return await _creatorService.Create<FollowDto>(new FollowRequest { FollowableId = id });
        }

        [SwaggerOperation(Summary = "Delete a category follow by unique id")]
        [Route("api/category-follows/{id:int}")]
        [HttpDelete]
        public async Task<ServiceResponse> Delete(int id)
        {
            await _deleterService.Delete(id);

            return ServiceResponse.Success("Resource has been deleted");
        }
    }
}
