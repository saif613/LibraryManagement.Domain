using AutoMapper;
using LibraryManagement.Application.DTOs.Requests;
using LibraryManagement.Application.DTOs.Responses;
using LibraryManagement.Application.Interfaces.ServiceInterfaces;
using LibraryManagement.Application.Interfaces.UnitOfWork;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LibraryManagement.Infrastructure.Identity
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AuthService(UserManager<User> userManager, IConfiguration configuration,IUnitOfWork unitOfWork,
                           IMapper mapper)
        {
            _userManager = userManager; 
            _configuration = configuration;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<AuthModel> RegisterAsync(RegisterDto model)
        {
            if (await _userManager.FindByEmailAsync(model.Email) != null)
                return new AuthModel { Message = "Email is already registered!" };

            var user = new User(model.Name, model.Email, "");
            user.UserName = model.Email;

            var result = await _userManager.CreateAsync(user, model.Password); 

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return new AuthModel { Message = errors };
            }

            await _userManager.AddToRoleAsync(user, "Member"); 


            return new AuthModel
            {
                IsAuthenticated = true,
                Message = "User registered successfully!",
                Email = user.Email!,
                Username = user.UserName,
                Roles = new List<string> { "Member" },
                Token = await CreateJwtToken(user) 
            };
        }

        public async Task<AuthModel> LoginAsync(LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email); 

            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
                return new AuthModel { Message = "Invalid Email or Password!" };

            var roles = await _userManager.GetRolesAsync(user);

            return new AuthModel
            {
                IsAuthenticated = true,
                Email = user.Email!,
                Username = user.UserName!,
                Roles = roles.ToList(),
                Token = await CreateJwtToken(user),
                Message = "Login Successful!"
            };
        }

        public async Task<string> LogoutAsync()
        {
            return "Logged out successfully. Please remove the token from client storage."; 
        }

        private async Task<string> CreateJwtToken(User user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.UserName!),
            new Claim(ClaimTypes.Email, user.Email!),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

            foreach (var role in roles)
                authClaims.Add(new Claim(ClaimTypes.Role, role));

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]!));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                expires: DateTime.Now.AddDays(Convert.ToDouble(_configuration["JWT:DurationInDays"])),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public async Task<UserResponse> GetUserProfileAsync(int userId)
        {
            var user = await _unitOfWork.Users.GetUserWithBorrows(userId);

            if (user == null)
                throw new KeyNotFoundException("User not found");

            
            return _mapper.Map<UserResponse>(user);
        }
    }
}
