using LibraryManagement.Application.DTOs.Requests;
using LibraryManagement.Application.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.Application.Interfaces.ServiceInterfaces
{
    public interface IBookService
    {
        Task<PagedResponse<BookResponse>> GetBooksPagedAsync(int pageNumber, CancellationToken ct = default);
        Task<BookResponse?> GetBookByIdAsync(int id, CancellationToken ct = default);
        Task<BookResponse> CreateBookAsync(CreateBookRequest request, CancellationToken ct = default);
        Task UpdateBookAsync(UpdateBookMetadataRequest request, CancellationToken ct = default);
        Task SoftDeleteBookAsync(int id, CancellationToken ct = default);
        Task<IEnumerable<BookResponse>> SearchBooksByItemAsync(string item, CancellationToken ct = default);
    }
}
