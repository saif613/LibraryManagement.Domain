using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.Application.DTOs.Responses
{
    public class ReviewResponse
    {
        public int UserId { get; set; }
        public string? Comment { get; set; }
        public int Rating { get; set; }
    }
}
