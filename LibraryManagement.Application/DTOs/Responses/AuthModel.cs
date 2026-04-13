using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.Application.DTOs.Responses
{
    public class AuthModel
    {
        public string Message { get; set; } = null!;     
        public bool IsAuthenticated { get; set; }       
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public List<string> Roles { get; set; } = new(); 
        public string Token { get; set; } = null!;     
        public DateTime ExpiresOn { get; set; }
    }
}
