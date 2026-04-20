using LibraryManagement.Application.Interfaces.Repositories;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Infrastructure.Repositories
{
    public class BorrowRepo : GenreircRepo<Borrow>, IBorrowRepo
    {
        public BorrowRepo(LibraryDbContext context) : base(context) { }

        public async Task<Borrow?> GetBorrowWithDetails(int id, CancellationToken ct = default) =>
            await _dbSet
                .AsNoTracking() 
                .Include(b => b.Book)
                .Include(b => b.User)
                .FirstOrDefaultAsync(b => b.Id == id, ct);

        public async Task<IEnumerable<Borrow>> GetUserBorrowHistory(int userId, CancellationToken ct = default) =>
            await _dbSet
                .AsNoTracking() 
                .Include(b => b.Book)
                .Include(b => b.User)
                .Where(b => b.UserId == userId)
                .OrderByDescending(b => b.CreatedAt)
                .ToListAsync(ct);

        public async Task<IEnumerable<Borrow>> GetActiveBorrows(CancellationToken ct = default) =>
            await _dbSet
                .AsNoTracking() 
                .Include(b => b.Book)
                .Include(b => b.User)
                .Where(b => b.Status == BorrowStatus.Borrowed)
                .ToListAsync(ct);

        public async Task<bool> IsBookAlreadyBorrowedByUser(int userId, int bookId, CancellationToken ct = default) =>
            await _dbSet.AnyAsync(b =>
                b.UserId == userId &&
                b.BookId == bookId &&
                b.ReturnDate == null, ct);

        public async Task<IEnumerable<Borrow>> GetReturnedToday(CancellationToken ct = default)
        {
            var today = DateTime.Today;

            return await _dbSet
                .AsNoTracking() 
                .Include(b => b.Book)
                .Include(b => b.User)
                .Where(b => b.ReturnDate.HasValue &&
                            b.ReturnDate.Value.Date == today)
                .ToListAsync(ct);
        }

        public async Task<IEnumerable<Borrow>> GetOverdueBorrowsAsync(DateTime now, CancellationToken ct = default)
        {
            return await _dbSet
                .AsNoTracking() 
                .Include(b => b.Book)
                .Include(b => b.User)
                .Where(b =>
                    b.Status == BorrowStatus.Borrowed &&
                    b.DueDate < now)
                .ToListAsync(ct);
        }

        public async Task<bool> HasOverdueBorrowsAsync(int userId, CancellationToken ct = default) =>
            await _dbSet.AnyAsync(b =>
                b.UserId == userId &&
                b.Status == BorrowStatus.Overdue, ct);

        public async Task<Borrow?> GetActiveBorrowByIdAsync(int bookId, int userId, CancellationToken ct = default)
        {
            return await _dbSet
                .Include(b => b.Book)
                .Include(b => b.User)
                .FirstOrDefaultAsync(b =>
                    b.UserId == userId &&
                    b.BookId == bookId &&
                    b.Status == BorrowStatus.Borrowed, ct);
        }

        public async Task<(IEnumerable<Borrow> Data, int TotalCount)> GetPagedBorrowsAsync(int pageNumber, int pageSize, CancellationToken ct)
        {
            var query = _dbSet
                .AsNoTracking() 
                .Include(b => b.User)
                .Include(b => b.Book);

            var totalCount = await query.CountAsync(ct);

            var data = await query
                .OrderByDescending(b => b.BorrowDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(ct);

            return (data, totalCount);
        }
    }
}