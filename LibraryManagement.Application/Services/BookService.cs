using AutoMapper;
using FluentValidation;
using LibraryManagement.Application.DTOs.Requests;
using LibraryManagement.Application.DTOs.Responses;
using LibraryManagement.Application.Interfaces.Repositories;
using LibraryManagement.Application.Interfaces.ServiceInterfaces;
using LibraryManagement.Application.Interfaces.UnitOfWork;
using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Application.Services
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BookService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BookResponse> CreateBookAsync(CreateBookRequest request, CancellationToken ct = default)
        {
            var category = await _unitOfWork.Categories.GetById(request.CategoryId, ct);
            if (category == null)
                throw new KeyNotFoundException("The specified Category does not exist.");

            var book = _mapper.Map<Book>(request);

            await _unitOfWork.Books.Create(book, ct);
            await _unitOfWork.SaveChangesAsync(ct);

            return await GetBookByIdAsync(book.Id, ct) ?? _mapper.Map<BookResponse>(book);
        }
        public async Task<PagedResponse<BookResponseForGetAll>> GetBooksPagedAsync(int pageNumber, CancellationToken ct = default)
        {
            int pageSize = 10;

            var (books, totalCount) = await _unitOfWork.Books.GetPagedBooksAsync(pageNumber, pageSize, ct);

            var bookDtos = _mapper.Map<List<BookResponseForGetAll>>(books);

            return new PagedResponse<BookResponseForGetAll>(bookDtos, pageNumber, pageSize, totalCount);
        }

        public async Task<BookResponse?> GetBookByIdAsync(int id, CancellationToken ct = default)
        {
            if (id < 0) 
                throw new ArgumentException("Invalid book ID.");

            var book = await _unitOfWork.Books.GetBookWithDetailsAsync(id, ct); 
            
            if (book == null || book.IsDeleted)
                throw new KeyNotFoundException("Book not found.");

            return _mapper.Map<BookResponse>(book);
        }

        public async Task<BookResponseForSearch> SearchBookByItemAsync(string item, CancellationToken ct = default)
        {
            var query = item.Trim().ToLower();
            var books = await _unitOfWork.Books.SearchBookWithDetailsAsync(query, ct);
            return _mapper.Map<BookResponseForSearch>(books);
        }

        public async Task UpdateBookAsync(UpdateBookMetadataRequest request, CancellationToken ct = default)
        {
            var existingBook = await _unitOfWork.Books.GetById(request.Id, ct);
            if (existingBook == null) throw new Exception("Book not found");

            if (!string.IsNullOrEmpty(request.ISBN) && request.ISBN != existingBook.ISBN)
            {
                var isIsbnDuplicate = await _unitOfWork.Books.AnyAsync(b => b.ISBN == request.ISBN && b.Id != request.Id, ct);
                if (isIsbnDuplicate)
                    throw new Exception("This ISBN is already assigned to another book."); 
            }

            if (!string.IsNullOrEmpty(request.Title) && request.Title != existingBook.Title)
            {
                var isTitleDuplicate = await _unitOfWork.Books.AnyAsync(b => b.Title == request.Title && b.Id != request.Id, ct);
                if (isTitleDuplicate)
                    throw new Exception("A book with the same title already exists.");
            }

            _mapper.Map(request, existingBook);

            await _unitOfWork.SaveChangesAsync(ct);
        }

        public async Task SoftDeleteBookAsync(int id, CancellationToken ct = default)
        {
            var book = await _unitOfWork.Books.GetBookWithDetailsAsync(id, ct);
            if (book == null) throw new KeyNotFoundException("Book not found.");

            var isCurrentlyBorrowed = book.borrows.Any(b => b.ReturnDate == null && !b.IsDeleted);

            if (isCurrentlyBorrowed)
            {
                throw new InvalidOperationException("Cannot delete the book because it is currently borrowed. It must be returned first.");
            }

            _unitOfWork.Books.SoftDelete(book);

            foreach (var review in book.Reviews)
            {
                _unitOfWork.Reviews.SoftDelete(review);
            }

            await _unitOfWork.SaveChangesAsync(ct);
        }
    }
}