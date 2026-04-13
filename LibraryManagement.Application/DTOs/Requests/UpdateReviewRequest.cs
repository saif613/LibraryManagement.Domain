using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.Application.DTOs.Requests
{
    public class UpdateReviewRequest
    {
        public int Rating { get; set; }
        public string Comment { get; set; } = string.Empty;
    }
}
