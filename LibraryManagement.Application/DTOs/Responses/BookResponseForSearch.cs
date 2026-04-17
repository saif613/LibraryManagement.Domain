using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.DTOs.Responses
{
    public class BookResponseForSearch
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Author { get; set; }
        public string? ISBN { get; set; }
        public List<ReviewResponse> Reviews { get; set; } = new();
        public string? CategoryName { get; set; }
    }
}
