using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.Application.DTOs.Responses
{
    public class BookResponse
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Author { get; set; }
        public string? ISBN { get; set; }
        public List<ReviewResponse> Reviews { get; set; } = new();
        public List<BorrowResponse> Borrows { get; set; } = new();
        public string? CategoryName { get; set; }
    }
}
