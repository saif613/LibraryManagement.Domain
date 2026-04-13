using LibraryManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace LibraryManagement.Application.Interfaces.Repositories
{
    public interface ICategoryRepo : IGenreircRepo<Category>
    {
        Task<Category?> GetCategoryWithBooksDetailsAsync(int id, CancellationToken ct);
        Task<Category?> GetCategoryByIdAsync(int id, CancellationToken ct = default);
        Task<(IEnumerable<Category> Data, int TotalCount)> GetPagedCategoriesAsync(int pageNumber, int pageSize, CancellationToken ct);
        Task<Category?> GetSingleByExpressionAsyncForCategory(Expression<Func<Category?, bool>> expression, CancellationToken ct = default);
    }
}
