using AutoMapper;
using LibraryManagement.Application.DTOs.Requests;
using LibraryManagement.Application.DTOs.Responses;
using LibraryManagement.Application.Exceptions;
using LibraryManagement.Application.Interfaces.ServiceInterfaces;
using LibraryManagement.Application.Interfaces.UnitOfWork;
using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Application.Services
{
    public class BorrowService : IBorrowService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public BorrowService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<int> ProcessOverdueBorrowsAsync(CancellationToken ct)
        {
            var overdueItems = await _unitOfWork.Borrows.GetOverdueBorrowsAsync(DateTime.UtcNow, ct);

            foreach (var borrow in overdueItems)
                borrow.MarkAsOverdue();

            await _unitOfWork.SaveChangesAsync(ct);

            return overdueItems.Count();
        }

        public async Task<MemberBorrowResponse> BorrowBookAsync(BorrowRequest request, int userId, CancellationToken ct = default)
        {
            var IsThisUserHasBook = await _unitOfWork.Borrows.IsBookAlreadyBorrowedByUser(request.BookId, userId, ct);

            if (IsThisUserHasBook)
                throw new InvalidOperationException("This User has already borrowed this book and has not returned it yet.");

            var book = await _unitOfWork.Books.GetById(request.BookId, ct);

            if (book == null)
                throw new KeyNotFoundException("Book not found.");

            if (book.StockQuantity <= 0)
                throw new ArgumentException("Book is out of stock.");

            var hasOverdue = await _unitOfWork.Borrows.HasOverdueBorrowsAsync(userId);

            if (hasOverdue)
                throw new InvalidOperationException("Sorry, borrowing is restricted because you have overdue books that haven't been returned yet.");

            book.ReduceStock(1);

            var borrowEntity = new Borrow(userId, request.BookId);

            await _unitOfWork.Borrows.Create(borrowEntity, ct);
            await _unitOfWork.SaveChangesAsync(ct);

            var result = await _unitOfWork.Borrows.GetBorrowWithDetails(borrowEntity.Id);
            return _mapper.Map<MemberBorrowResponse>(result);
        }

        public async Task<IEnumerable<BorrowResponse>> GetActiveBorrowsAsync(CancellationToken ct)
        {
            var ActiveBorrows = await _unitOfWork.Borrows.GetActiveBorrows(ct);
            return _mapper.Map<IEnumerable<BorrowResponse>>(ActiveBorrows);
        }

        public async Task<PagedResponse<BorrowResponse>> GetBorrowsPagedAsync(int pageNumber, CancellationToken ct = default)
        {
            int pageSize = 10;

            var (borrows, totalCount) = await _unitOfWork.Borrows.GetPagedBorrowsAsync(pageNumber, pageSize, ct);

            var borrowDtos = _mapper.Map<List<BorrowResponse>>(borrows);

            return new PagedResponse<BorrowResponse>(borrowDtos, pageNumber, pageSize, totalCount);
        }

        public async Task<BorrowResponse?> GetBorrowByIdAsync(int id, CancellationToken ct)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid borrow ID.");

            var borrow = await _unitOfWork.Borrows.GetBorrowWithDetails(id);
            return _mapper.Map<BorrowResponse?>(borrow);
        }

        public async Task<IEnumerable<BorrowResponse>> GetReturnedTodayAsync(CancellationToken ct)
        {
            var returnedToday = await _unitOfWork.Borrows.GetReturnedToday(ct);
            return _mapper.Map<IEnumerable<BorrowResponse>>(returnedToday);
        }

        public async Task<IEnumerable<MemberBorrowHistoryResponse>> GetUserHistoryAsync(int userId, CancellationToken ct)
        {
            var userHistory = await _unitOfWork.Borrows.GetUserBorrowHistory(userId, ct);
            return _mapper.Map<IEnumerable<MemberBorrowHistoryResponse>>(userHistory);
        }

        public async Task<bool> ReturnBookAsync(BorrowRequest request, int userId, CancellationToken ct)
        {
            var activeBorrow = await _unitOfWork.Borrows.GetActiveBorrowByIdAsync(request.BookId, userId);

            if (activeBorrow == null)
                throw new BadRequestException("you cannot return this book you have not already loan it");

            activeBorrow.MarkAsReturned();

            if (activeBorrow.Book != null)
                activeBorrow.Book.IncreaseStock(1);

            await _unitOfWork.SaveChangesAsync(ct);
            return true;
        }

        public async Task<MemberBorrowResponse> RenewBorrow(BorrowRequest request, int userId, CancellationToken ct)
        {
            var activeBorrow = await _unitOfWork.Borrows.GetActiveBorrowByIdAsync(request.BookId, userId);

            if (activeBorrow == null)
                throw new ArgumentException("You cannot renew this book because you do not have an active loan for it.");

            activeBorrow.RenewBorrow(14);

            await _unitOfWork.SaveChangesAsync(ct);

            return _mapper.Map<MemberBorrowResponse>(activeBorrow);
        }
    }
}
