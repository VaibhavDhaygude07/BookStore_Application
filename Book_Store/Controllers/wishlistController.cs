using Bussiness_Layer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Book_Store.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class wishlistController : ControllerBase
    {
        private readonly IWishlistService _wishlistService;

        public wishlistController(IWishlistService wishlistService)
        {
            _wishlistService = wishlistService;
        }

        [HttpPost]
        public async Task<IActionResult> AddToWishlist(int bookId)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized(new { success = false, message = "User not authenticated", data = (object)null });

            int userId = int.Parse(userIdClaim.Value);

            var result = await _wishlistService.AddToWishlist(userId, bookId);
            if (result != null)
                return Ok(new { success = true, message = "Book added to wishlist", data = result });

            return BadRequest(new { success = false, message = "Failed to add to wishlist", data = (object)null });
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveFromWishlist(int bookId)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized(new { success = false, message = "User not authenticated", data = (object)null });

            int userId = int.Parse(userIdClaim.Value);

            var result = await _wishlistService.RemoveFromWishlist(userId, bookId);
            if (result)
                return Ok(new { success = true, message = "Book removed from wishlist", data = (object)null });

            return NotFound(new { success = false, message = "Book not found in wishlist", data = (object)null });
        }

        [HttpGet]
        public async Task<IActionResult> GetWishlist()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized(new { success = false, message = "User not authenticated", data = (object)null });

            int userId = int.Parse(userIdClaim.Value);

            var result = await _wishlistService.GetWishlistByUser(userId);
            return Ok(new { success = true, message = "Wishlist retrieved successfully", data = result });
        }
    }
}
