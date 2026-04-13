using LibraryManagement.Application.DTOs.Requests;
using LibraryManagement.Application.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.Application.Interfaces.ServiceInterfaces
{
    public interface IReviewService
    {
        Task AddReviewAsync(int userId, ReviewRequest reviewRequest, CancellationToken ct = default);
        Task<IEnumerable<ReviewResponse>> GetReviewsByBookIdAsync(int bookId, CancellationToken ct = default);
        Task<double> GetAverageRatingForBookAsync(int bookId, CancellationToken ct = default);
        Task<IEnumerable<ReviewResponse>> GetLatestReviewsAsync(int count, CancellationToken ct = default);
        Task UpdateReviewAsync(int reviewId, int userId, UpdateReviewRequest request, CancellationToken ct = default);
        Task DeleteReviewAsync(int reviewId, CancellationToken ct = default);
        Task<PagedResponse<ReviewResponse>> GetAllReviewsPagedAsync(int pageNumber, CancellationToken ct = default);
        Task<ReviewResponse?> GetReviewByIdAsync(int reviewId, CancellationToken ct = default);
    }
}
