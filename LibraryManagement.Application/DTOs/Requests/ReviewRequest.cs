using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.Application.DTOs.Requests
{
    public class ReviewRequest
    {
        public int BookId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; } = string.Empty;
    }
}
