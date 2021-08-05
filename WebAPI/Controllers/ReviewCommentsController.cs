using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Core.Interfaces;
using Storage.Models;
using Core.Requests;
using Core.Response;

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

        [HttpGet("{reviewCommentId:int}")]
        public async Task<ServiceResponse> GetComment(int reviewCommentId)
        {
            return await _reviewCommentService.GetComment(reviewCommentId);
        }

        [HttpGet]
        public async Task<ServiceResponse> GetComments(int? reviewId, Guid? userId, bool currentUser)
        {
            return await _reviewCommentService.GetComments(reviewId, userId, currentUser);
        }

        [HttpPost]
        public async Task<ServiceResponse> Create(ReviewCommentRequest comment)
        {
            return await _reviewCommentService.AddReviewComment(comment);
        }

        [HttpPut("{id:int}")]
        public async Task<ServiceResponse> Edit(int id, ReviewCommentRequest comment)
        {
            await _crud.Update(comment, id);
            return ServiceResponse.Success("Comment updated.");
        }

        [HttpDelete("{id:int}")]
        public async Task<ServiceResponse> Delete(int id)
        {
            await _crud.Delete(id);
            return ServiceResponse.Success("Comment deleted.");
        }
    }
}
