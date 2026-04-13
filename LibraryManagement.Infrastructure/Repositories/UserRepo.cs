using LibraryManagement.Application.Interfaces.Repositories;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.Infrastructure.Repositories
{
    public class UserRepo : GenreircRepo<User>, IUserRepo
    {
        public UserRepo(LibraryDbContext context) : base(context) { }

        public async Task<User?> GetUserWithBorrows(int id) =>
                await _dbSet
                .Include(u => u.Borrows)
                .ThenInclude(b => b.Book)
                .FirstOrDefaultAsync(u => u.Id == id);

    }
}
