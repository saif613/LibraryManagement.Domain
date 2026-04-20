using LibraryManagement.Application.Interfaces.Repositories;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LibraryManagement.Infrastructure.Repositories
{
    public class GenreircRepo<T> : IGenreircRepo<T> where T : class, IBaseEntity
    {
        protected readonly LibraryDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenreircRepo(LibraryDbContext context, CancellationToken ct = default)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task Create(T item, CancellationToken ct = default) =>
            await _dbSet.AddAsync(item);

        public async Task<T?> GetById(int id, CancellationToken ct = default) =>
            await _dbSet.FindAsync(id); 
        public void SoftDelete(T item, CancellationToken ct = default)
        {
            item.IsDeleted = true;
            _dbSet.Update(item);
        }

        public void Update(T item) =>
            _dbSet.Update(item);

        public async Task<T?> GetSingleByExpressionAsync(Expression<Func<T, bool>> expression, CancellationToken ct = default) =>
            await _dbSet
                .AsNoTracking() 
                .FirstOrDefaultAsync(expression);

        public async Task<IEnumerable<T>> GetSomeByExpressionAsync(Expression<Func<T, bool>> expression, CancellationToken ct = default) =>
            await _dbSet
                .AsNoTracking()
                .Where(expression)
                .ToListAsync();

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = default) =>
            await _context.Set<T>().AnyAsync(predicate, ct);

        public async Task<(List<T> Data, int TotalCount)> GetPagedAsync(int pageNumber, int pageSize, CancellationToken ct = default)
        {
            var query = _context.Set<T>()
                .AsNoTracking(); 

            var totalCount = await query.CountAsync(ct);

            var data = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(ct);

            return (data, totalCount);
        }
    }
}