using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LibraryManagement.Application.DTOs.Responses
{
    public class MemberBorrowHistoryResponse
    {
        public string  BookTitle { get; set; } = string.Empty;
        
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DateTime? BorrowDate { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DateTime? ReturnDate { get; set; }

        public string Status { get; set; } = string.Empty;
    }
}
