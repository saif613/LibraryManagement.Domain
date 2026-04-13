using LibraryManagement.Application.DTOs.Requests;
using LibraryManagement.Application.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.Application.Interfaces.ServiceInterfaces
{
    public interface IAuthService
    {
        Task<AuthModel> RegisterAsync(RegisterDto model);

        Task<AuthModel> LoginAsync(LoginDto model);

        Task<string> LogoutAsync();

        Task<UserResponse> GetUserProfileAsync(int userId);
    }
}
