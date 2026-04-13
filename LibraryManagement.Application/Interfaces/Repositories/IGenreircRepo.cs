using LibraryManagement.Domain.Entities;
using System.Linq.Expressions;

namespace LibraryManagement.Application.Interfaces.Repositories
{
    public interface IGenreircRepo<T> where T : IBaseEntity
    {
        Task Create(T item, CancellationToken ct = default);
        void Update(T item);
        void SoftDelete(T item, CancellationToken ct = default);
        Task<T?> GetById(int id, CancellationToken ct = default);
        Task<T?> GetSingleByExpressionAsync(Expression<Func<T, bool>> expression, CancellationToken ct = default);
        Task<IEnumerable<T>> GetSomeByExpressionAsync(Expression<Func<T, bool>> expression, CancellationToken ct = default);
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = default);
        Task<(List<T> Data, int TotalCount)> GetPagedAsync(int pageNumber, int pageSize, CancellationToken ct = default);
    }
}
