using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebAPI.DTOs;
using Core.Interfaces;
using Storage.Models;
using Core.Exceptions;
using Core.Requests;
using Core.ServiceResponses;
using System.Net;
using System.Collections.Generic;
using Core.DTOs;

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

        [HttpGet("{id:int}")]
        public async Task<ServiceResponse> GetComment(int id)
        {
            var result = await _crud.GetById<ReviewCommentDto>(id);
            if (result == null) return new SuccessResponse() { Message = "Comment not found." };
            return new SuccessResponse<ReviewCommentDto>() { Message = "Comment found.", Content = result };
        }

        [HttpGet]
        public async Task<ServiceResponse> GetComments(int? reviewId, int? userId, bool? currentUser)
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
            return new SuccessResponse() { Message = "Comment updated." };
        }

        [HttpDelete("{id:int}")]
        public async Task<ServiceResponse> Delete(int id)
        {
            await _crud.Delete(id);
            return new SuccessResponse() { Message = "Comment deleted." };
        }
    }
}
