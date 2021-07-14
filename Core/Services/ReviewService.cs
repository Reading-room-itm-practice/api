using AutoMapper;
using Core.DTOs;
using Core.Interfaces;
using Core.ServiceResponses;
using Microsoft.EntityFrameworkCore;
using Storage.DataAccessLayer;
using Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    class ReviewService : IReviewService
    {
        private readonly ICrudService<Review> _crud;
        private readonly ApiDbContext _apiDbContext;
        private readonly IMapper _mapper;

        public ReviewService(ICrudService<Review> crud, ApiDbContext apiDbContext, IMapper mapper)
        {
            _crud = crud;
            _apiDbContext = apiDbContext;
            _mapper = mapper;
        }

        public async Task<ServiceResponse> GetReviews(int? bookId)
        {
            if (bookId != null)
            {
                var book = _apiDbContext.Books.Include(b => b.Reviews).FirstOrDefault(b => b.Id == bookId);
                if(book == null || book.Reviews.Count == 0) return new SuccessResponse()
                    { Message = $"Reviews for Book with ID = {bookId} not found.", StatusCode = HttpStatusCode.OK };

                var reviews = _mapper.Map<ICollection<ReviewDto>>(book.Reviews);
                return new SuccessResponse<ICollection<ReviewDto>>()
                    { Message = $"Reviews for Book with ID = {bookId} retrieved.", StatusCode = HttpStatusCode.OK, Content = reviews };
            }
            else
            {
                var reviews = await _crud.GetAll<ReviewDto>();
                return new SuccessResponse<IEnumerable<ReviewDto>>()
                    { Message = "Reviews retrieved.", StatusCode = HttpStatusCode.OK, Content = reviews };
            }
        }
    }
}
