using AutoMapper;
using Core.Common;
using Core.DTOs;
using Core.Interfaces;
using Core.Requests;
using Core.Response;
using Microsoft.AspNetCore.Identity;
using Storage.Identity;
using Storage.Interfaces;
using Storage.Models;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Core.Services
{
    class ReviewService : IReviewService
    {
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;
        private readonly IReviewRepository _reviewRepository;
        private readonly IGetterService<Book> _bookGetter;
        private readonly ILoggedUserProvider _loggedUserProvider;

        public ReviewService(IMapper mapper, IUriService uriService, IReviewRepository reviewRepository, IGetterService<Book> bookGetter, ILoggedUserProvider loggedUserProvider)
        {
            _mapper = mapper;
            _uriService = uriService;
            _loggedUserProvider = loggedUserProvider;
            _reviewRepository = reviewRepository;
            _bookGetter = bookGetter;
        }

        public async Task<ServiceResponse> AddReview(ReviewRequest review)
        {
            if (await _bookGetter.GetById<BookDto>(review.BookId) == null)
            {
                return ServiceResponse.Error($"Book with Id: {review.BookId} doesn't exist", HttpStatusCode.BadRequest);
            }

            var userId = _loggedUserProvider.GetUserId();

            if (await _reviewRepository.ReviewByUserExists(userId, review.BookId))
            {
                return ServiceResponse.Error($"You have already posted a review for book Id: {review.BookId}", HttpStatusCode.BadRequest);
            }

            var reviewRequest = _mapper.Map<Review>(review);
            var newReview = _mapper.Map<ReviewDto>(await _reviewRepository.CreateReview(reviewRequest));

            return ServiceResponse<ReviewDto>.Success(newReview, "Review created.", HttpStatusCode.Created);
        }

        public async Task<ServiceResponse> GetReviews(PaginationFilter filter, string route)
        {
            var reviews = _mapper.Map<DataDto<ReviewDto>>(await _reviewRepository.GetReviews(filter));
            var pagedResponse = PaginationHelper.CreatePagedReponse(reviews.data, filter, reviews.count, _uriService, route);

            return ServiceResponse<PagedResponse<IEnumerable<ReviewDto>>>.Success(pagedResponse, $"All reviews retrieved.");
        }
    }
}

