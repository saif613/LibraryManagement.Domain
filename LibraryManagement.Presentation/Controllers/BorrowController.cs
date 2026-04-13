using LibraryManagement.Application.DTOs.Requests;
using LibraryManagement.Application.Interfaces.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LibraryManagement.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BorrowController : ControllerBase
    {
        private readonly IBorrowService _borrowService;

        public BorrowController(IBorrowService borrowService)
        {
            _borrowService = borrowService;
        }

        [HttpPost("borrow")]
        public async Task<IActionResult> BorrowBook([FromBody] BorrowRequest request, CancellationToken ct)
        {
            var userId = GetCurrentUserId();
            var result = await _borrowService.BorrowBookAsync(request, userId, ct);
            return Ok(result);
        }

        [HttpPost("return")]
        public async Task<IActionResult> ReturnBook([FromBody] BorrowRequest request, CancellationToken ct)
        {
            var userId = GetCurrentUserId();
            var result = await _borrowService.ReturnBookAsync(userId, request, ct);
            return Ok(new { success = result, message = "Book returned successfully." });
        }

        [HttpPost("renew")]
        public async Task<IActionResult> RenewBorrow([FromBody] BorrowRequest request, CancellationToken ct)
        {
            var userId = GetCurrentUserId();
            var result = await _borrowService.RenewBorrow(request, userId, ct);
            return Ok(result);
        }

        [HttpGet("my-history")]
        public async Task<IActionResult> GetMyHistory(CancellationToken ct)
        {
            var userId = GetCurrentUserId();
            var history = await _borrowService.GetUserHistoryAsync(userId, ct);
            return Ok(history);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllBorrowsPaged([FromQuery] int page = 1, CancellationToken ct = default)
        {
            var result = await _borrowService.GetBorrowsPagedAsync(page, ct);
            return Ok(result);
        }

        [HttpGet("active")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetActiveBorrows(CancellationToken ct)
        {
            var result = await _borrowService.GetActiveBorrowsAsync(ct);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetBorrowById(int id, CancellationToken ct)
        {
            var result = await _borrowService.GetBorrowByIdAsync(id, ct);
            if (result == null) return NotFound("Borrow record not found.");

            return Ok(result);
        }

        [HttpGet("returned-today")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetReturnedToday(CancellationToken ct)
        {
            var result = await _borrowService.GetReturnedTodayAsync(ct);
            return Ok(result);
        }

        [HttpPost("process-overdue")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ProcessOverdueBorrows(CancellationToken ct)
        {
            await _borrowService.ProcessOverdueBorrowsAsync(ct);
            return Ok(new { message = "Overdue borrows processed successfully." });
        }
        private int GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null) throw new UnauthorizedAccessException("User ID is missing form token.");
            return int.Parse(userIdClaim.Value);
        }
    }
}
