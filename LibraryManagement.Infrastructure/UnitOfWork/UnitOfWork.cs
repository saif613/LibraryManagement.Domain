using LibraryManagement.Application.Interfaces.Repositories;
using LibraryManagement.Application.Interfaces.UnitOfWork;
using LibraryManagement.Infrastructure.Persistence;
using LibraryManagement.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LibraryDbContext _context;
        public IUserRepo Users { get; }
        public IBookRepo Books { get; }
        public ICategoryRepo Categories { get; }
        public IBorrowRepo Borrows { get; }
        public IReviewRepo Reviews { get; }
        public UnitOfWork(LibraryDbContext context)
        {
            _context = context;
            Users = new UserRepo(_context);
            Books = new BookRepo(_context);
            Categories = new CategoryRepo(_context);
            Reviews = new ReviewRepo(_context);
            Borrows = new BorrowRepo(_context);
        }
        public void Dispose() => _context.Dispose();
        public async Task<int> SaveChangesAsync(CancellationToken ct = default) => await _context.SaveChangesAsync(ct);
    }
}
