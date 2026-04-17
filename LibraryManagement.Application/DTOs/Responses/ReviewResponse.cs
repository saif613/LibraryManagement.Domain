using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace LibraryManagement.Application.DTOs.Responses
{
    public class ReviewResponse
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int ?UserId { get; set; }
        public string? Comment { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? Rating { get; set; }
    }
}
