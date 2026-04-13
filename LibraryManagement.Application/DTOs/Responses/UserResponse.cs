using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.Application.DTOs.Responses
{
    public class UserResponse
    {
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? UserName { get; set; }

        public int TotalBorrowedBooks { get; set; } 
        public int CurrentActiveBorrows { get; set; } 

        public List<UserBorrowDto> BorrowHistory { get; set; } = new List<UserBorrowDto>();
    }
}
