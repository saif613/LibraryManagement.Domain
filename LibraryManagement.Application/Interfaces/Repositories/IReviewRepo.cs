using LibraryManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.Application.Interfaces.Repositories
{
    public interface IReviewRepo : IGenreircRepo<Review>
    {
        Task<IEnumerable<Review>> GetReviewsByBookId(int bookId, CancellationToken ct = default);
        Task<double> GetAverageRatingForBook(int bookId, CancellationToken ct = default);
        Task<IEnumerable<Review>> GetLatestReviews(int count, CancellationToken ct = default);
        Task<(IEnumerable<Review> Data, int TotalCount)> GetPagedReviewsAsync(int page, int size, CancellationToken ct);
        Task<bool> HasUserReviewedBookAsync(int userId, int reviewId, CancellationToken ct = default);
    }
}
