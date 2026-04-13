using LibraryManagement.Application.DTOs.Requests;
using LibraryManagement.Application.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.Application.Interfaces.ServiceInterfaces
{
    public interface IBorrowService
    {
        Task<BorrowResponse> BorrowBookAsync(BorrowRequest request, int userId, CancellationToken ct);
        Task<bool> ReturnBookAsync(int userId, BorrowRequest request, CancellationToken ct);
        Task<BorrowResponse?> GetBorrowByIdAsync(int id, CancellationToken ct);
        Task<BorrowResponse> RenewBorrow(BorrowRequest request, int userId, CancellationToken ct);
        Task<PagedResponse<BorrowResponse>> GetBorrowsPagedAsync(int pageNumber, CancellationToken ct = default);
        Task<IEnumerable<BorrowResponse>> GetActiveBorrowsAsync(CancellationToken ct);
        Task<IEnumerable<BorrowResponse>> GetReturnedTodayAsync(CancellationToken ct);
        Task<IEnumerable<BorrowResponse>> GetUserHistoryAsync(int userId, CancellationToken ct);
        Task ProcessOverdueBorrowsAsync(CancellationToken ct);
    }
}
