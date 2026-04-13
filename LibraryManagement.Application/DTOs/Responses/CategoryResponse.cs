using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.Application.DTOs.Responses
{
    public class CategoryResponse
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int NumberOfBooks { get; set; } 
        
    }
}
