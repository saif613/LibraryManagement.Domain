using LibraryManagement.Application.Interfaces.Repositories;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace LibraryManagement.Infrastructure.Repositories
{
    public class ReviewRepo : GenreircRepo<Review>, IReviewRepo
    {
        public ReviewRepo(LibraryDbContext context) : base(context) { }

        public async Task<IEnumerable<Review>> GetReviewsByBookId(int bookId, CancellationToken ct = default) =>
             await _dbSet
            .AsNoTracking()
                .Include(r => r.User)
                .Where(r => r.BookId == bookId)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync(ct);

        public async Task<double> GetAverageRatingForBook(int bookId, CancellationToken ct = default)
        {
            var reviews = _dbSet.Where(r => r.BookId == bookId).AsNoTracking();

            if (!await reviews.AnyAsync(ct))
                return 0;

            return await reviews.AverageAsync(r => r.Rating, ct);
        }

        public async Task<IEnumerable<Review>> GetLatestReviews(int count, CancellationToken ct = default) =>
            await _dbSet
            .AsNoTracking()
                .Include(r => r.Book)
                .Include(r => r.User)
                .OrderByDescending(r => r.CreatedAt)
                .Take(count)
                .ToListAsync(ct);

        public async Task<(IEnumerable<Review> Data, int TotalCount)> GetPagedReviewsAsync(int page, int size, CancellationToken ct)
        {
            var query = _dbSet
                .AsNoTracking()
                .Include(r => r.User)
                .Include(r => r.Book);

            var totalCount = await query.CountAsync(ct);

            var data = await query
                .OrderByDescending(r => r.CreatedAt)
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync(ct);

            return (data, totalCount);
        }

        public async Task<bool> HasUserReviewedBookAsync(int userId, int bookId, CancellationToken ct = default)
        {
            return await _dbSet.AnyAsync(r => r.UserId == userId && r.BookId == bookId, ct);
        }
    }
}
