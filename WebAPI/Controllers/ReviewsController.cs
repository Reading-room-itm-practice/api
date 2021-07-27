using Core.DTOs;
using Core.Interfaces;
using Core.Requests;
using Core.ServiceResponses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Storage.Identity;
using Storage.Models;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly ICrudService<Review> _crud;
        private readonly IReviewService _reviewService;
        

        public ReviewsController(ICrudService<Review> crud, IReviewService reviewService)
        {
            _crud = crud;
            _reviewService = reviewService;
        }

        [HttpGet("{id:int}")]
        public async Task<ServiceResponse> GetReview(int id)
        {
            var result = await _crud.GetById<ReviewDto>(id);
            if (result == null)
            { 
                ServiceResponse.Error("Review not found.");
            }

            return result;
        }

        [HttpGet]
        public async Task<ServiceResponse> GetReviews(int? bookId)
        {
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

            return ServiceResponse.Success("Review updated.");
        }

        [HttpDelete("{id:int}")]
        public async Task<ServiceResponse> Delete(int id)
        {
            await _crud.Delete(id);

            return ServiceResponse.Success("Review deleted.");
        }
    }
}
