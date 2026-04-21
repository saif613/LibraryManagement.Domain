using LibraryManagement.Application.DTOs.Requests;
using LibraryManagement.Application.Interfaces.ServiceInterfaces;
using LibraryManagement.Presentation.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LibraryManagement.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;
        private readonly ICurrentUserService _currentUserService;

        public ReviewController(IReviewService reviewService, ICurrentUserService currentUserService)
        {
            _reviewService = reviewService;
            _currentUserService = currentUserService;
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddReview([FromBody] ReviewRequest request, CancellationToken ct)
        {
            var userId = _currentUserService.UserId;
            await _reviewService.AddReviewAsync(userId, request, ct);
            return Ok(new { message = "Review added successfully." });
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateReview(int id, [FromBody] UpdateReviewRequest request, CancellationToken ct)
        {
            var userId = _currentUserService.UserId;
            await _reviewService.UpdateReviewAsync(id, userId, request, ct);
            return Ok(new { message = "Review updated successfully." });
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteReview(int id, CancellationToken ct)
        {
            await _reviewService.DeleteReviewAsync(id, ct);
            return Ok(new { message = "Review deleted successfully." });
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetReviewById(int id, CancellationToken ct)
        {
            var review = await _reviewService.GetReviewByIdAsync(id, ct);
            return Ok(review);
        }

        [HttpGet("book/{bookId}")]
        [Authorize]
        public async Task<IActionResult> GetReviewsByBookId(int bookId, CancellationToken ct)
        {
            var reviews = await _reviewService.GetReviewsByBookIdAsync(bookId, ct);
            return Ok(reviews);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllReviews([FromQuery] int pageNumber = 1, CancellationToken ct = default)
        {
            var response = await _reviewService.GetAllReviewsPagedAsync(pageNumber, ct);
            return Ok(response);
        }

        [HttpGet("latest")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetLatestReviews([FromQuery] int count = 5, CancellationToken ct = default)
        {
            var reviews = await _reviewService.GetLatestReviewsAsync(count, ct);
            return Ok(reviews);
        }

        [HttpGet("book/{bookId}/rating")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAverageRating(int bookId, CancellationToken ct)
        {
            var averageRating = await _reviewService.GetAverageRatingForBookAsync(bookId, ct);
            return Ok(new { averageRating = averageRating });
        }
    }
}
