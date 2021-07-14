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

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly ICrudService<Review> _crud;

        public ReviewController(ICrudService<Review> crud)
        {
            _crud = crud;
        }

        [HttpGet("{id:int}")]
        public async Task<ServiceResponse> GetReview(int id)
        {
            var result = await _crud.GetById<ReviewDto>(id);
            if (result == null) return new SuccessResponse() { Message = "Review not found.", StatusCode = HttpStatusCode.OK };
            return new SuccessResponse<ReviewDto>() { Message = "Review found.", StatusCode = HttpStatusCode.OK, Content = result };
        }

        [HttpGet]
        public async Task<ServiceResponse> GetReviews(int? bookId)
        {
            if (bookId != null)
            {
                var result = _crud.GetAll<ReviewDto>().Result.Where(r => r.BookId == bookId);
                return new SuccessResponse<IEnumerable<ReviewDto>>() 
                    { Message = $"Reviews for Book with ID = {bookId} retrieved.", StatusCode = HttpStatusCode.OK, Content = result };
            }
            else
            {
                var result = await _crud.GetAll<ReviewDto>();
                return new SuccessResponse<IEnumerable<ReviewDto>>() 
                    { Message = "Reviews retrieved.", StatusCode = HttpStatusCode.OK, Content = result };
            }
        }

        [HttpPost]
        public async Task<ServiceResponse> Create(ReviewRequest review)
        {
            var newReview = await _crud.Create<ReviewDto>(review);
            return new SuccessResponse<ReviewDto>()
                { Message = "Review created.", StatusCode = HttpStatusCode.Created, Content = newReview };
        }

        [HttpPut("{id:int}")]
        public async Task<ServiceResponse> Edit(int id, ReviewRequest review)
        {
            await _crud.Update(review, id);
            return new SuccessResponse()
                { Message = "Review updated.", StatusCode = HttpStatusCode.OK };
        }

        [HttpDelete("{id:int}")]
        public async Task<ServiceResponse> Delete(int id)
        {
            await _crud.Delete(id);
            return new SuccessResponse()
                { Message = "Review deleted.", StatusCode = HttpStatusCode.OK };
        }
    }
}
