using LibraryManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.Application.Interfaces.Repositories
{
    public interface IBookRepo : IGenreircRepo<Book>
    {
        Task<Book?> GetBookWithDetailsAsync(int id, CancellationToken ct = default);
        Task<Book>SearchBookWithDetailsAsync(string query, CancellationToken ct = default);
        Task<(IEnumerable<Book> Data, int TotalCount)> GetPagedBooksAsync(int pageNumber, int pageSize, CancellationToken ct);
    }
}
