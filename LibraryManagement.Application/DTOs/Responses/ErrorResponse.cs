using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.Application.DTOs.Responses
{
    public class ErrorResponse
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public string? Details { get; set; }
    }
}
