using AutoMapper;
using LibraryManagement.Application.DTOs.Requests;
using LibraryManagement.Application.DTOs.Responses;
using LibraryManagement.Application.Exceptions;
using LibraryManagement.Application.Interfaces.ServiceInterfaces;
using LibraryManagement.Application.Interfaces.UnitOfWork;
using LibraryManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.Application.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ReviewService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task AddReviewAsync(int userId, ReviewRequest reviewRequest, CancellationToken ct = default)
        {
            var hasBorrowed = await _unitOfWork.Borrows
                .IsBookAlreadyBorrowedByUser(userId, reviewRequest.BookId, ct);

            var hasReturned = await _unitOfWork.Borrows
                .HasUserReturnedBookAsync(userId, reviewRequest.BookId, ct);

            if (!hasBorrowed && !hasReturned)
                throw new BadRequestException("You must borrow this book before adding a review.");

            if (hasBorrowed && !hasReturned)
                throw new BadRequestException("You must return the book before adding a review.");

            if (reviewRequest == null)
                throw new ArgumentNullException(nameof(reviewRequest));


            var book = await _unitOfWork.Books.GetById(reviewRequest.BookId, ct);
            if (book == null)
                throw new KeyNotFoundException("Book not found.");

            bool exists = await _unitOfWork.Reviews.HasUserReviewedBookAsync(userId, reviewRequest.BookId, ct);
            if (exists)
                throw new InvalidOperationException("User has already reviewed this book.");

            var review = _mapper.Map<Review>(reviewRequest);

            review.SetUser(userId);

            await _unitOfWork.Reviews.Create(review, ct);
            await _unitOfWork.SaveChangesAsync(ct);
        }

        public async Task DeleteReviewAsync(int reviewId, CancellationToken ct = default)
        {
            var review = await _unitOfWork.Reviews.GetById(reviewId, ct);
            if (review == null)
                throw new KeyNotFoundException("Review not found.");

            _unitOfWork.Reviews.SoftDelete(review);
            await _unitOfWork.SaveChangesAsync(ct);
        }

        public async Task<PagedResponse<ReviewResponse>> GetAllReviewsPagedAsync(int pageNumber, CancellationToken ct = default)
        {
            int pageSize = 20;

            var (review, totalCount) = await _unitOfWork.Reviews.GetPagedReviewsAsync(pageNumber, pageSize, ct);
            var ReviewDtos = _mapper.Map<List<ReviewResponse>>(review);

            return new PagedResponse<ReviewResponse>(ReviewDtos, pageNumber, pageSize, totalCount);
        }

        public Task<double> GetAverageRatingForBookAsync(int bookId, CancellationToken ct = default)
        {
            return _unitOfWork.Reviews.GetAverageRatingForBook(bookId);
        }

        public async Task<IEnumerable<ReviewResponse>> GetLatestReviewsAsync(int count, CancellationToken ct = default)
        {
            var reviews = await _unitOfWork.Reviews.GetLatestReviews(count, ct);
            var reviewDtos = _mapper.Map<IEnumerable<ReviewResponse>>(reviews);
            return reviewDtos;
        }

        public async Task<ReviewResponse?> GetReviewByIdAsync(int reviewId, CancellationToken ct = default)
        {
            var review = await _unitOfWork.Reviews.GetById(reviewId, ct);

            if (review == null)
                throw new KeyNotFoundException($"Review with ID {reviewId} not found.");

            return _mapper.Map<ReviewResponse>(review);
        }

        public async Task<IEnumerable<ReviewResponse>> GetReviewsByBookIdAsync(int bookId, CancellationToken ct = default)
        {
            var reviewsOnBook = await _unitOfWork.Reviews.GetReviewsByBookId(bookId, ct);
            return _mapper.Map<IEnumerable<ReviewResponse>>(reviewsOnBook);
        }

        public async Task UpdateReviewAsync(int reviewId, int userId, UpdateReviewRequest request, CancellationToken ct = default)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var review = await _unitOfWork.Reviews.GetById(reviewId, ct);

            if (review == null)
                throw new KeyNotFoundException($"Review with ID {reviewId} not found.");

            if (review.UserId != userId)
                throw new InvalidOperationException("Access denied. You can only update reviews that you have created.");

             _mapper.Map(request, review);

            await _unitOfWork.SaveChangesAsync(ct);
        }
    }
}
