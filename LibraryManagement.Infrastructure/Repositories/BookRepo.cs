using LibraryManagement.Application.Interfaces.Repositories;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.Infrastructure.Repositories
{
    public class BookRepo : GenreircRepo<Book>, IBookRepo
    {
        public BookRepo(LibraryDbContext context) : base(context) { }

        public async Task<Book?> GetBookWithDetailsAsync(int id, CancellationToken ct = default) =>
         await _dbSet
        .Include(b => b.Category)
        .Include(b => b.Reviews)
        .Include(b => b.borrows)
        .ThenInclude(br => br.User)
        .FirstOrDefaultAsync(b => b.Id == id, ct);

        public async Task<Book> SearchBookWithDetailsAsync(string query, CancellationToken ct = default)
        {
            return await _dbSet
                .Include(b => b.Category)
                .Include(b => b.Reviews)
                .Where(b => (b.Title.ToLower().Contains(query) ||
                             b.Author.ToLower().Contains(query) ||
                             b.ISBN.Contains(query)) && !b.IsDeleted)
                .AsNoTracking()
                .FirstOrDefaultAsync(ct) ?? throw new KeyNotFoundException($"No book found matching the query: {query}");
        }
        public async Task<(IEnumerable<Book> Data, int TotalCount)> GetPagedBooksAsync(int pageNumber, int pageSize, CancellationToken ct)
        {
            var query = _dbSet
                .AsNoTracking()
                .Include(b => b.Category);

            var totalCount = await query.CountAsync(ct);

            var data = await query
                .OrderBy(b => b.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsSplitQuery()
                .ToListAsync(ct);

            return (data, totalCount);
        }
        public async Task<bool> ExistsByIsbnAsync(string isbn, int? excludeId = null, CancellationToken ct = default)
        {
            return await _context.Books
                .AnyAsync(b => b.ISBN == isbn && (excludeId == null || b.Id != excludeId), ct);
        }


        public async Task<bool> ExistsByTitleAsync(string title, int? excludeId = null, CancellationToken ct = default)
        {
            return await _context.Books
                .AnyAsync(b => b.Title == title && (excludeId == null || b.Id != excludeId), ct);
        }

        public async Task<bool> ExistsByUrlAsync(string url, int? excludeId = null, CancellationToken ct = default)
        {
            return await _context.Books
                .AnyAsync(b => b.URL == url && (excludeId == null || b.Id != excludeId), ct);
        }

    }
}
