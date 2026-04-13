using LibraryManagement.Application.Interfaces.Repositories;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace LibraryManagement.Infrastructure.Repositories
{
    public class CategoryRepo : GenreircRepo<Category>, ICategoryRepo
    {
        public CategoryRepo(LibraryDbContext context) : base(context) { }

        public async Task<Category?> GetCategoryWithBooksDetailsAsync(int id, CancellationToken ct = default) =>
            await _dbSet
                .Include(c => c.Books)
                    .ThenInclude(b => b.borrows)
                    .FirstOrDefaultAsync(c => c.Id == id, ct);

        public async Task<Category?> GetCategoryByIdAsync(int id, CancellationToken ct = default) =>
               await _dbSet.Include(c => c.Books).AsNoTracking().FirstOrDefaultAsync(c => c.Id == id, ct);
        public async Task<(IEnumerable<Category> Data, int TotalCount)> GetPagedCategoriesAsync(int pageNumber, int pageSize, CancellationToken ct)
        {
            var query = _dbSet
                .AsNoTracking()
                .Include(c => c.Books);

            var totalCount = await query.CountAsync(ct);

            var data = await query
                .OrderBy(c => c.Name) 
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(ct);

            return (data, totalCount);
        }

        public async Task<Category?> GetSingleByExpressionAsyncForCategory(Expression<Func<Category?, bool>> expression, CancellationToken ct = default) =>
          await _dbSet.Include(c => c.Books).AsNoTracking().FirstOrDefaultAsync(expression);
    }
}
