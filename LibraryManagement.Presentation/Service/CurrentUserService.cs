using LibraryManagement.Application.Interfaces.ServiceInterfaces;
using System.Security.Claims;

namespace LibraryManagement.Presentation.Service
{
    public class CurrentUserService: ICurrentUserService
    {

        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int UserId
        {
            get
            {
                var userIdClaim = _httpContextAccessor.HttpContext?.User?
                    .FindFirst(ClaimTypes.NameIdentifier);

                if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out var userId))
                    throw new UnauthorizedAccessException("Invalid user ID in token.");

                return userId;
            }
        }
    }
}
