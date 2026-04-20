using LibraryManagement.Application.DTOs.Requests;
using LibraryManagement.Application.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.Application.Interfaces.ServiceInterfaces
{
    public interface IBorrowService
    {
        Task<MemberBorrowResponse> BorrowBookAsync(BorrowRequest request, int userId, CancellationToken ct);
        Task<bool> ReturnBookAsync(BorrowRequest request, int userId, CancellationToken ct);
        Task<BorrowResponse?> GetBorrowByIdAsync(int id, CancellationToken ct);
        Task<MemberBorrowResponse> RenewBorrow(BorrowRequest request, int userId, CancellationToken ct);
        Task<PagedResponse<BorrowResponse>> GetBorrowsPagedAsync(int pageNumber, CancellationToken ct = default);
        Task<IEnumerable<BorrowResponse>> GetActiveBorrowsAsync(CancellationToken ct);
        Task<IEnumerable<BorrowResponse>> GetReturnedTodayAsync(CancellationToken ct);
        Task<IEnumerable<MemberBorrowHistoryResponse>> GetUserHistoryAsync(int userId, CancellationToken ct);
        Task<int> ProcessOverdueBorrowsAsync(CancellationToken ct);
    }
}
