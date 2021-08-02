using Core.Interfaces;
using Core.ServiceResponses;
using Microsoft.AspNetCore.Mvc;
using Storage.Models.Likes;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebAPI.Controllers.Likes
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentLikesController : ControllerBase
    {
        private readonly IDeleterService<ReviewCommentLike> _deleterService;
        private readonly IGetterByCreatorService<ReviewCommentLike> _getterService;
        private readonly ICreatorService<ReviewCommentLike> _creatorService;

        public CommentLikesController(IDeleterService<ReviewCommentLike> deleter, IGetterByCreatorService<ReviewCommentLike> getter, ICreatorService<ReviewCommentLike> creator)
        {
            _deleterService = deleter;
            _getterService = getter;
            _creatorService = creator;
        }

        [SwaggerOperation(Description = "Retrieves all authors follows")]
        [Route("api/users/{id:guid}/author-follows")]
        [HttpGet]
        public async Task<ServiceResponse> Index(Guid id)
        {
            return await _getterService.GetAllByCreator<FollowDto>(id);
        }

        [SwaggerOperation(Description = "Create author follow for logged user")]
        [Route("api/authors/{id:int}/follows")]
        [HttpPost]
        public async Task<ServiceResponse> Create(int id)
        {
            return await _creatorService.Create<FollowDto>(
                new LikeRequest { FollowableId = id, CreatorId = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier)) });
        }

        [SwaggerOperation(Description = "Delete a follow by unique id")]
        [Route("api/author-follows/{id:int}")]
        [HttpDelete]
        public async Task<ServiceResponse> Delete(int id)
        {
            await _deleterService.Delete(id);

            return ServiceResponse.Success("Resource has been deleted");
        }
    }
}
