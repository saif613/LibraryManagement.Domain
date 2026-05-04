using AutoMapper;
using FluentAssertions;
using LibraryManagement.Application.DTOs.Requests;
using LibraryManagement.Application.DTOs.Responses;
using LibraryManagement.Application.Interfaces.UnitOfWork;
using LibraryManagement.Application.Services;
using LibraryManagement.Domain.Entities;
using Moq;
using System.Linq.Expressions;
using System.Reflection;

namespace LibraryManagement.Tests
{
    public class CategoryServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly CategoryService _service;

        public CategoryServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();

            _service = new CategoryService(
                _unitOfWorkMock.Object,
                _mapperMock.Object
            );
        }

        [Fact]
        public async Task GetCategoryById_ShouldThrow_WhenNotFound()
        {
            // Arrange
            _unitOfWorkMock.Setup(x => x.Categories.GetCategoryByIdAsync(1, default))
                .ReturnsAsync((Category?)null);

            // Act
            Func<Task> act = async () =>
                await _service.GetCategoryByIdAsync(1);

            // Assert
            await act.Should().ThrowAsync<KeyNotFoundException>();
        }

        [Fact]
        public async Task GetCategoryById_ShouldReturnCategory_WhenExists()
        {
            // Arrange
            var category = (Category)Activator.CreateInstance(typeof(Category), true)!;
            var response = new CategoryResponse { Id = 1, Name = "Test" };

            _unitOfWorkMock.Setup(x => x.Categories.GetCategoryByIdAsync(1, default))
                .ReturnsAsync(category);

            _mapperMock.Setup(x => x.Map<CategoryResponse>(category))
                .Returns(response);

            // Act
            var result = await _service.GetCategoryByIdAsync(1);

            // Assert
            result.Should().NotBeNull();
            result!.Name.Should().Be("Test");
        }
        [Fact]
        public async Task CreateCategory_ShouldThrow_WhenNameExists()
        {
            // Arrange
            var request = new CreateCategoryRequest { Name = "Test" };

            var existingCategory = (Category)Activator.CreateInstance(typeof(Category), true)!;

            _unitOfWorkMock.Setup(x => x.Categories
                .GetSingleByExpressionAsync(It.IsAny<Expression<Func<Category, bool>>>(), default))
                .ReturnsAsync(existingCategory);

            // Act
            Func<Task> act = async () =>
                await _service.CreateCategoryAsync(request);

            // Assert
            await act.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task CreateCategory_ShouldCreateSuccessfully()
        {
            // Arrange
            var request = new CreateCategoryRequest { Name = "Test" };

            var category = (Category)Activator.CreateInstance(typeof(Category), true)!;

            _unitOfWorkMock.Setup(x => x.Categories
                .GetSingleByExpressionAsync(It.IsAny<Expression<Func<Category, bool>>>(), default))
                .ReturnsAsync((Category?)null);

            _mapperMock.Setup(x => x.Map<Category>(request))
                .Returns(category);

            _mapperMock.Setup(x => x.Map<CategoryResponse>(category))
                .Returns(new CategoryResponse { Name = "Test" });

            // Act
            var result = await _service.CreateCategoryAsync(request);

            // Assert
            _unitOfWorkMock.Verify(x => x.Categories.Create(
    It.IsAny<Category>(),
    It.IsAny<CancellationToken>()
), Times.Once);
            _unitOfWorkMock.Verify(x => x.SaveChangesAsync(default), Times.Once);
            result.Name.Should().Be("Test");
        }

        [Fact]
        public async Task SoftDeleteCategory_ShouldThrow_WhenCategoryNotFound()
        {
            // Arrange
            _unitOfWorkMock.Setup(x => x.Categories
                .GetCategoryWithBooksDetailsAsync(1, default))
                .ReturnsAsync((Category?)null);

            // Act
            Func<Task> act = async () =>
                await _service.SoftDeleteCategoryAsync(1);

            // Assert
            await act.Should().ThrowAsync<KeyNotFoundException>();
        }

        [Fact]
        public async Task SoftDeleteCategory_ShouldThrow_WhenHasActiveLoans()
        {
            // Arrange
            var category = (Category)Activator.CreateInstance(typeof(Category), true)!;

            var book = (Book)Activator.CreateInstance(typeof(Book), true)!;
            var borrow = (Borrow)Activator.CreateInstance(typeof(Borrow), true)!;

            typeof(Borrow).GetProperty("ReturnDate")?.SetValue(borrow, null);
            typeof(Borrow).GetProperty("IsDeleted")?.SetValue(borrow, false);

            typeof(Book).GetProperty("borrows")?.SetValue(book, new List<Borrow> { borrow });
            typeof(Category).GetProperty("Books")?.SetValue(category, new List<Book> { book });

            _unitOfWorkMock.Setup(x => x.Categories
                .GetCategoryWithBooksDetailsAsync(1, default))
                .ReturnsAsync(category);

            // Act
            Func<Task> act = async () =>
                await _service.SoftDeleteCategoryAsync(1);

            // Assert
            await act.Should().ThrowAsync<InvalidOperationException>();
        }

        [Fact]
        public async Task SoftDeleteCategory_ShouldDeleteSuccessfully()
        {
            // Arrange
            var category = new Category("Test");


            typeof(Category).GetProperty("Books",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                ?.SetValue(category, new List<Book>());

            _unitOfWorkMock.Setup(x => x.Categories
                .GetCategoryWithBooksDetailsAsync(1, default))
                .ReturnsAsync(category);

            // Act
            await _service.SoftDeleteCategoryAsync(1);

            // Assert
            _unitOfWorkMock.Verify(x => x.Categories.SoftDelete(
                It.IsAny<Category>(),
                It.IsAny<CancellationToken>()
            ), Times.Once);

            _unitOfWorkMock.Verify(x => x.SaveChangesAsync(default), Times.Once);
        }
        [Fact]
        public async Task GetCategoriesPagedAsync_ShouldReturnPagedResult()
        {
            // Arrange
            var categories = new List<Category>
    {
        (Category)Activator.CreateInstance(typeof(Category), true)!,
        (Category)Activator.CreateInstance(typeof(Category), true)!
    };

            var mapped = new List<CategoryResponse>
    {
        new CategoryResponse { Name = "C1" },
        new CategoryResponse { Name = "C2" }
    };

            _unitOfWorkMock.Setup(x => x.Categories
                .GetPagedCategoriesAsync(1, 5, default))
                .ReturnsAsync((categories, 2));

            _mapperMock.Setup(x => x.Map<List<CategoryResponse>>(categories))
                .Returns(mapped);

            // Act
            var result = await _service.GetCategoriesPagedAsync(1, default);

            // Assert
            result.Should().NotBeNull();
            result.Data.Count.Should().Be(2);
            result.PageNumber.Should().Be(1);
        }

        [Fact]
        public async Task UpdateCategory_ShouldThrow_WhenNotFound()
        {
            // Arrange
            var request = new CreateCategoryRequest { Name = "Test" };

            _unitOfWorkMock.Setup(x => x.Categories.GetById(1, default))
                .ReturnsAsync((Category?)null);

            // Act
            Func<Task> act = async () =>
                await _service.UpdateCategoryAsync(1, request);

            // Assert
            await act.Should().ThrowAsync<KeyNotFoundException>();
        }

        [Fact]
        public async Task UpdateCategory_ShouldThrow_WhenNameExists()
        {
            // Arrange
            var request = new CreateCategoryRequest { Name = "Test" };

            var existing = (Category)Activator.CreateInstance(typeof(Category), true)!;

            _unitOfWorkMock.Setup(x => x.Categories.GetById(1, default))
                .ReturnsAsync(existing);

            _unitOfWorkMock.Setup(x => x.Categories
                .GetSomeByExpressionAsync(It.IsAny<Expression<Func<Category, bool>>>(), default))
                .ReturnsAsync(new List<Category> { existing });

            // Act
            Func<Task> act = async () =>
                await _service.UpdateCategoryAsync(1, request);

            // Assert
            await act.Should().ThrowAsync<InvalidOperationException>();
        }

        [Fact]
        public async Task UpdateCategory_ShouldUpdateSuccessfully()
        {
            // Arrange
            var request = new CreateCategoryRequest { Name = "Test" };

            var existing = (Category)Activator.CreateInstance(typeof(Category), true)!;

            _unitOfWorkMock.Setup(x => x.Categories.GetById(1, default))
                .ReturnsAsync(existing);

            _unitOfWorkMock.Setup(x => x.Categories
                .GetSomeByExpressionAsync(It.IsAny<Expression<Func<Category, bool>>>(), default))
                .ReturnsAsync(new List<Category>());

            // Act
            await _service.UpdateCategoryAsync(1, request);

            // Assert
            _mapperMock.Verify(x => x.Map(request, existing), Times.Once);

            _unitOfWorkMock.Verify(x => x.Categories.Update(existing), Times.Once);

            _unitOfWorkMock.Verify(x => x.SaveChangesAsync(default), Times.Once);
        }

        [Fact]
        public async Task SearchOneItem_ShouldThrow_WhenNotFound()
        {
            // Arrange
            _unitOfWorkMock.Setup(x => x.Categories
                .GetSingleByExpressionAsyncForCategory(It.IsAny<Expression<Func<Category?, bool>>>(), default))
                .ReturnsAsync((Category?)null);

            // Act
            Func<Task> act = async () =>
                await _service.SearchOneItemByNameAsync("Test");

            // Assert
            await act.Should().ThrowAsync<KeyNotFoundException>();
        }

        [Fact]
        public async Task SearchOneItem_ShouldReturnCategory_WhenExists()
        {
            // Arrange
            var category = (Category)Activator.CreateInstance(typeof(Category), true)!;

            var response = new CategoryResponse { Name = "Test" };

            _unitOfWorkMock.Setup(x => x.Categories
                .GetSingleByExpressionAsyncForCategory(It.IsAny<Expression<Func<Category?, bool>>>(), default))
                .ReturnsAsync(category);

            _mapperMock.Setup(x => x.Map<CategoryResponse>(category))
                .Returns(response);

            // Act
            var result = await _service.SearchOneItemByNameAsync("Test");

            // Assert
            result.Should().NotBeNull();
            result.Name.Should().Be("Test");
        }
    }
}