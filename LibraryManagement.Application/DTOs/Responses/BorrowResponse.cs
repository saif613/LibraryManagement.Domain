using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.Application.DTOs.Responses
{
    public class BorrowResponse
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public DateTime? BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
