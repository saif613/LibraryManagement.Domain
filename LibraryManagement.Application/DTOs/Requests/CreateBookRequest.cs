using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.Application.DTOs.Requests
{
    public class CreateBookRequest
    {
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? ISBN { get; set; }
        public string? Url { get; set; }
        public int CategoryId { get; set; }
    }
}
