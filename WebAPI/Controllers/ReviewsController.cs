using Core.DTOs;
using Core.Interfaces;
using Core.Requests;
using Core.Response;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IGettterPaginationService _getPaged;


        public ReviewsController(ICrudService<Review> crud, IReviewService reviewService, IGettterPaginationService getPaged)
        {
            _crud = crud;
            _reviewService = reviewService;
            _getPaged = getPaged;
        }

        [HttpGet("{id:int}")]
        public async Task<ServiceResponse> GetReview(int id)
        {
            var result = await _crud.GetById<ReviewDto>(id);

            return result;
        }

        [HttpGet]
        public async Task<ServiceResponse> GetReviews([FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            return await _getPaged.GetAll<Review, ReviewDto>(filter, route);
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
