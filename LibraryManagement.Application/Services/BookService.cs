using AutoMapper;
using FluentValidation;
using LibraryManagement.Application.DTOs.Requests;
using LibraryManagement.Application.DTOs.Responses;
using LibraryManagement.Application.Exceptions;
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

        public async Task<BookResponseForGetAllAndCreate> CreateBookAsync(CreateBookRequest request, CancellationToken ct = default)
        {

            if (await _unitOfWork.Books.ExistsByIsbnAsync(request.ISBN!, ct: ct))
                throw new ConflictException("ISBN already exists");

            if (await _unitOfWork.Books.ExistsByTitleAsync(request.Title!, ct: ct))
                throw new ConflictException("Title already exists");

            if (await _unitOfWork.Books.ExistsByUrlAsync(request.Url!, ct: ct))
                throw new ConflictException("URL already exists");

            var category = await _unitOfWork.Categories.GetById(request.CategoryId, ct);
            if (category == null)
                throw new KeyNotFoundException("The specified Category does not exist.");

            var book = _mapper.Map<Book>(request);

            await _unitOfWork.Books.Create(book, ct);
            await _unitOfWork.SaveChangesAsync(ct);
            return _mapper.Map<BookResponseForGetAllAndCreate>(book);
        }
        public async Task<PagedResponse<BookResponseForGetAllAndCreate>> GetBooksPagedAsync(int pageNumber, CancellationToken ct = default)
        {
            int pageSize = 10;

            var (books, totalCount) = await _unitOfWork.Books.GetPagedBooksAsync(pageNumber, pageSize, ct);

            var bookDtos = _mapper.Map<List<BookResponseForGetAllAndCreate>>(books);

            return new PagedResponse<BookResponseForGetAllAndCreate>(bookDtos, pageNumber, pageSize, totalCount);
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

        public async Task<BookResponseForUpdate> UpdateBookAsync(int id, UpdateBookMetadataRequest request, CancellationToken ct = default)
        {
            var existingBook = await _unitOfWork.Books.GetById(id, ct);

            if (existingBook == null)
                throw new NotFoundException("Book not found");

            if (request.ISBN != existingBook.ISBN &&
                await _unitOfWork.Books.ExistsByIsbnAsync(request.ISBN!, id, ct))
            {
                throw new ConflictException("ISBN already exists");
            }

            if (request.Title != existingBook.Title &&
              await _unitOfWork.Books.ExistsByTitleAsync(request.Title!, id, ct))
            {
                throw new ConflictException("Title already exists");
            }

            if (request.Url != existingBook.URL &&
             await _unitOfWork.Books.ExistsByUrlAsync(request.Url!, id, ct))
            {
                throw new ConflictException("URL already exists");
            }

            _mapper.Map(request, existingBook);

            await _unitOfWork.SaveChangesAsync(ct);

            return _mapper.Map<BookResponseForUpdate>(existingBook);
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