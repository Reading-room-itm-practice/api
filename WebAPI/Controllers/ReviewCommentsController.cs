using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Core.Interfaces;
using Storage.Models;
using Core.Requests;
using Core.Response;
using Swashbuckle.AspNetCore.Annotations;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewCommentsController : ControllerBase
    {
        private readonly ICrudService<ReviewComment> _crud;
        private readonly IReviewCommentService _reviewCommentService;

        public ReviewCommentsController(ICrudService<ReviewComment> crud, IReviewCommentService reviewCommentService)
        {
            _crud = crud;
            _reviewCommentService = reviewCommentService;
        }

        [SwaggerOperation(Summary = "Retrieves all comments for a specyfic review")]
        [HttpGet("All")]
        public async Task<ServiceResponse> GetComments(int? reviewId, Guid? userId, bool currentUser)
        {
            return await _reviewCommentService.GetComments(reviewId, userId, currentUser);
        }

        [SwaggerOperation(Summary = "Retrieves specific comment by unique id")]
        [HttpGet("{reviewCommentId:int}")]
        public async Task<ServiceResponse> GetComment(int reviewCommentId)
        {
            return await _reviewCommentService.GetComment(reviewCommentId);
        }

        [SwaggerOperation(Summary = "Create comment for a specyfic review")]
        [HttpPost("Create")]
        public async Task<ServiceResponse> Create(ReviewCommentRequest comment)
        {
            return await _reviewCommentService.AddReviewComment(comment);
        }

        [SwaggerOperation(Summary = "Edit comment for a specyfic review")]
        [HttpPut("Edit/{id:int}")]
        public async Task<ServiceResponse> Edit(int id, ReviewCommentRequest comment)
        {
            await _crud.Update(comment, id);
            return ServiceResponse.Success("Comment updated.");
        }

        [SwaggerOperation(Summary = "Delete specyfic comment by unique id")]
        [HttpDelete("Delete/{id:int}")]
        public async Task<ServiceResponse> Delete(int id)
        {
            await _crud.Delete(id);
            return ServiceResponse.Success("Comment deleted.");
        }
    }
}
