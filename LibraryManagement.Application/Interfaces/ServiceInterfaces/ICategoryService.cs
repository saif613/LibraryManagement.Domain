using LibraryManagement.Application.DTOs.Requests;
using LibraryManagement.Application.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.Application.Interfaces.ServiceInterfaces
{
    public interface ICategoryService
    {
        Task<PagedResponse<CategoryResponse>> GetCategoriesPagedAsync(int pageNumber, CancellationToken ct);
        Task<CategoryResponse?> GetCategoryByIdAsync(int id, CancellationToken ct = default);
        Task<CategoryResponse> CreateCategoryAsync(CreateCategoryRequest request, CancellationToken ct = default);
        Task UpdateCategoryAsync(int id, CreateCategoryRequest request, CancellationToken ct = default);
        Task SoftDeleteCategoryAsync(int id, CancellationToken ct = default);
        Task<CategoryResponse> SearchOneItemByNameAsync(string name, CancellationToken ct = default);
    }
}
