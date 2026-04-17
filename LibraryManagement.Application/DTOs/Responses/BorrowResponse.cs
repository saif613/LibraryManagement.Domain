using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace LibraryManagement.Application.DTOs.Responses
{
    public class BorrowResponse
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? UserId { get; set; }

        public string? UserName { get; set; } = string.Empty;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DateTime? BorrowDate { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DateTime? ReturnDate { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
