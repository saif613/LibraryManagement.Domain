using LibraryManagement.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.Application.Interfaces.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepo Users { get; }
        IBookRepo Books { get; }
        ICategoryRepo Categories { get; }
        IBorrowRepo Borrows { get; }
        IReviewRepo Reviews { get; }
        Task<int> SaveChangesAsync(CancellationToken ct = default);
    }
}
