using Core.DTOs;
using Core.Interfaces;
using Core.Requests;
using Core.ServiceResponses;
using Microsoft.AspNetCore.Mvc;
using Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.ServiceModel;
using Storage.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Storage.Interfaces;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly ICrudService<Review> _crud;
        private readonly IReviewService _reviewService;
        

        public ReviewController(ICrudService<Review> crud, IReviewService reviewService, ILoggedUserProvider loggedUserProvider)
        {
            _crud = crud;
            _reviewService = reviewService;
        }

        [HttpGet("{id:int}")]
        public async Task<ServiceResponse> GetReview(int id)
        {
            var result = await _crud.GetById<ReviewDto>(id);
            if (result == null) return new SuccessResponse() { Message = "Review not found." };
            return new SuccessResponse<ReviewDto>() { Message = "Review found.", Content = result };
        }

        [Authorize]
        [HttpGet]
        public async Task<ServiceResponse> GetReviews(int? bookId)
        {
            var user = User.Identity;
            int a;
            return await _reviewService.GetReviews(bookId);
        }

        [HttpPost]
        public async Task<ServiceResponse> Create(ReviewRequest review)
        {
            return await _reviewService.AddReview(review);
        }

        [HttpPut("{id:int}")]
        public async Task<ServiceResponse> Edit(int id, ReviewRequest review)
        {
            await _crud.Update(review, id);
            return new SuccessResponse() { Message = "Review updated." };
        }

        [HttpDelete("{id:int}")]
        public async Task<ServiceResponse> Delete(int id)
        {
            await _crud.Delete(id);
            return new SuccessResponse() { Message = "Review deleted." };
        }
    }
}
