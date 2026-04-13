using AutoMapper;
using FluentValidation;
using LibraryManagement.Application.DTOs.Requests;
using LibraryManagement.Application.DTOs.Responses;
using LibraryManagement.Application.Interfaces.ServiceInterfaces;
using LibraryManagement.Application.Interfaces.UnitOfWork;
using LibraryManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PagedResponse<CategoryResponse>> GetCategoriesPagedAsync(int pageNumber, CancellationToken ct)
        {
            int pageSize = 5; 
            var (categories, totalCount) = await _unitOfWork.Categories.GetPagedCategoriesAsync(pageNumber, pageSize, ct);
            var categoryDtos = _mapper.Map<List<CategoryResponse>>(categories);

            return new PagedResponse<CategoryResponse>(categoryDtos, pageNumber, pageSize, totalCount);
        }

        public async Task<CategoryResponse?> GetCategoryByIdAsync(int id, CancellationToken ct = default)
        {
            var category = await _unitOfWork.Categories.GetCategoryByIdAsync(id, ct);

            if (category == null)
                throw new KeyNotFoundException("Category not found");

            return _mapper.Map<CategoryResponse>(category);
        }

        public async Task<CategoryResponse> CreateCategoryAsync(CreateCategoryRequest request, CancellationToken ct = default)
        {
            var isExist = await _unitOfWork.Categories.GetSingleByExpressionAsync(x => x.Name.ToLower() == request.Name.ToLower(), ct);

            if (isExist != null)
                throw new ArgumentException("This category name already exists.");

            var categoryEntity = _mapper.Map<Category>(request);

            await _unitOfWork.Categories.Create(categoryEntity);
            await _unitOfWork.SaveChangesAsync(ct);

            return _mapper.Map<CategoryResponse>(categoryEntity);
        }

        public async Task UpdateCategoryAsync(int id, CreateCategoryRequest request, CancellationToken ct = default)
        {

            var existingCategory = await _unitOfWork.Categories.GetById(id, ct);

            if (existingCategory == null)
                throw new KeyNotFoundException("Category not found");

            
            var isNameExists = await _unitOfWork.Categories.GetSomeByExpressionAsync(
                c => c.Name.ToLower() == request.Name.ToLower() && c.Id != id, ct);

            if (isNameExists.Any())
            {
                throw new InvalidOperationException("Category name already exists in another category.");
            }

              _mapper.Map(request, existingCategory);

            _unitOfWork.Categories.Update(existingCategory);
            await _unitOfWork.SaveChangesAsync(ct);
        }

        public async Task SoftDeleteCategoryAsync(int id, CancellationToken ct = default)
        {

            var category = await _unitOfWork.Categories.GetCategoryWithBooksDetailsAsync(id, ct);

            if (category == null) throw new KeyNotFoundException("Category not found");


            var hasActiveLoans = category.Books.Any(b =>
                b.borrows.Any(br => br.ReturnDate == null && !br.IsDeleted));

            if (hasActiveLoans)
            {
                throw new InvalidOperationException("Cannot delete category. Some books are currently borrowed.");
            }

            _unitOfWork.Categories.SoftDelete(category);
            foreach (var book in category.Books)
            {
                _unitOfWork.Books.SoftDelete(book);
            }

            await _unitOfWork.SaveChangesAsync(ct);
        }
        public async Task<CategoryResponse> SearchOneItemByNameAsync(string name, CancellationToken ct = default)
        {
            var category = await _unitOfWork.Categories.GetSingleByExpressionAsyncForCategory(
                x => x!.Name.ToLower() == name.ToLower(),
                ct
            );

            if (category == null)
                throw new KeyNotFoundException($"Category with name '{name}' not found");

            return _mapper.Map<CategoryResponse>(category);
        }
    }
}
