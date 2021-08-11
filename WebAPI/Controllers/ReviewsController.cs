using Core.DTOs;
using Core.Interfaces;
using Core.Requests;
using Core.Response;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using Storage.Models;
using Swashbuckle.AspNetCore.Annotations;
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

        [SwaggerOperation(Summary = "Retrieves all reviews")]
        [HttpGet("All")]
        public async Task<ServiceResponse> GetReviews([FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            return await _getPaged.GetAll<Review, ReviewDto>(filter, route);
        }

        [SwaggerOperation(Summary = "Retrieves specific review by unique id")]
        [HttpGet("{reviewId:int}")]
        public async Task<ServiceResponse> GetReview(int reviewId)
        {
            return await _reviewService.GetReview(reviewId);
        }

        [SwaggerOperation(Summary = "Create new reviwe for a specyfic book")]
        [HttpPost("Create")]
        public async Task<ServiceResponse> Create(ReviewRequest review)
        {
            return await _reviewService.AddReview(review);
        }

        [SwaggerOperation(Summary = "Edit specific review by unique id")]
        [HttpPut("Edit/{id:int}")]
        public async Task<ServiceResponse> Edit(int id, ReviewRequest review)
        {
            await _crud.Update(review, id);
            return ServiceResponse.Success("Review updated.");
        }

        [SwaggerOperation(Summary = "Delete specific review by unique id")]
        [HttpDelete("Delete/{id:int}")]
        public async Task<ServiceResponse> Delete(int id)
        {
            await _crud.Delete(id);
            return ServiceResponse.Success("Review deleted.");
        }
    }
}
