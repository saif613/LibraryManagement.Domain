using LibraryManagement.Application.DTOs.Requests;
using LibraryManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.Application.Interfaces.Repositories
{
    public interface IBorrowRepo : IGenreircRepo<Borrow>
    {
        Task<Borrow?> GetBorrowWithDetails(int id, CancellationToken ct = default);
        Task<IEnumerable<Borrow>> GetUserBorrowHistory(int userId, CancellationToken ct = default);
        Task<IEnumerable<Borrow>> GetActiveBorrows(CancellationToken ct = default);
        Task<bool> IsBookAlreadyBorrowedByUser(int userId, int bookId, CancellationToken ct = default);
        Task<IEnumerable<Borrow>> GetReturnedToday(CancellationToken ct = default);
        Task<IEnumerable<Borrow>> GetOverdueBorrowsAsync(DateTime now, CancellationToken ct = default);
        
        Task<bool> HasOverdueBorrowsAsync(int userId, CancellationToken ct = default);
        Task<Borrow?> GetActiveBorrowByIdAsync(int bookId, int userId, CancellationToken ct = default);
        Task<(IEnumerable<Borrow> Data, int TotalCount)> GetPagedBorrowsAsync(int pageNumber, int pageSize, CancellationToken ct);
    }
}
