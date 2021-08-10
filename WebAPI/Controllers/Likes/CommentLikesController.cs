using System;
using Core.Interfaces;
using Core.ServiceResponses;
using Microsoft.AspNetCore.Mvc;
using Storage.Models.Likes;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;
using Core.DTOs;
using Core.DTOs.Follows;
using Core.Requests;

namespace WebAPI.Controllers.Likes
{
    [ApiController]
    [Route("api/comments")]
    public class CommentLikesController : ControllerBase
    {
        private readonly IDeleterService<ReviewCommentLike> _deleterService;
        private readonly ICreatorService<ReviewCommentLike> _creatorService;

        public CommentLikesController(IDeleterService<ReviewCommentLike> deleter,  ICreatorService<ReviewCommentLike> creator)
        {
            _deleterService = deleter;
            _creatorService = creator;
        }


        [SwaggerOperation(Summary = "Create category like for logged user")]
        [Route("{id:int}/likes")]
        [HttpPost]
        public async Task<ServiceResponse> Create(int id)
        {
            return await _creatorService.Create<FollowDto>(new LikeRequest());
        }


        [SwaggerOperation(Description = "Delete a like by unique id")]
        [Route("likes/{id:int}")]
        [HttpDelete]
        public async Task<ServiceResponse> Delete(int id)
        {
            await _deleterService.Delete(id);

            return ServiceResponse.Success("Resource has been deleted");
        }
    }
}
