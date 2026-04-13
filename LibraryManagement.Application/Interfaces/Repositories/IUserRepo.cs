using LibraryManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.Application.Interfaces.Repositories
{
    public interface IUserRepo
    {
        Task<User?> GetUserWithBorrows(int id);
    }
}
