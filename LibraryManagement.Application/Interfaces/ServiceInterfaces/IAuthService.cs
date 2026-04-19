using LibraryManagement.Application.DTOs.Requests;
using LibraryManagement.Application.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.Application.Interfaces.ServiceInterfaces
{
    public interface IAuthService
    {
        Task<AuthModel> RegisterAsync(RegisterDto model, CancellationToken ct = default);

        Task<AuthModel> LoginAsync(LoginDto model, CancellationToken ct = default);

        Task<string> LogoutAsync();

        Task<UserResponse> GetUserProfileAsync(int userId, CancellationToken ct = default);
    }
}
