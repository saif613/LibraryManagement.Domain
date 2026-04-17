using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.Application.DTOs.Requests
{
    public class UpdateBookMetadataRequest
    {
        public string? ISBN { get; set; } 
        public string? Title { get; set; }
        public string? Url { get; set; }
    }
}
