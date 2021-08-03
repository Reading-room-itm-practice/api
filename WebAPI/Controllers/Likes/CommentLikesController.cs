using System;
using Core.Interfaces;
using Core.ServiceResponses;
using Microsoft.AspNetCore.Mvc;
using Storage.Models.Likes;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;
using Core.DTOs;
using Core.Requests;

namespace WebAPI.Controllers.Likes
{
    [ApiController]
    [Route("api")]
    public class CommentLikesController : ControllerBase
    {
        private readonly IDeleterService<ReviewCommentLike> _deleterService;
        private readonly IGetterService<ReviewCommentLike> _getterService;
        private readonly ICreatorService<ReviewCommentLike> _creatorService;

        public CommentLikesController(IDeleterService<ReviewCommentLike> deleter, IGetterService<ReviewCommentLike> getter, ICreatorService<ReviewCommentLike> creator)
        {
            _deleterService = deleter;
            _getterService = getter;
            _creatorService = creator;
        }


        [SwaggerOperation(Summary = "Create category follow for logged user")]
        [Route("comments/{id:int}/likes")]
        [HttpPost]
        public async Task<ServiceResponse> Create(int id)
        {
            return await _creatorService.Create<LikeDto>(new LikeRequest());
        }


        [SwaggerOperation(Description = "Delete a follow by unique id")]
        [Route("comments/likes/{id:int}")]
        [HttpDelete]
        public async Task<ServiceResponse> Delete(int id)
        {
            await _deleterService.Delete(id);

            return ServiceResponse.Success("Resource has been deleted");
        }
    }
}
