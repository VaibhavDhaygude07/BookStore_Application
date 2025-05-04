using Bussiness_Layer.Interfaces;
using Bussiness_Layer.Services;
using DataAccess_Layer.DTO_s;
using DataAccess_Layer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Book_Store.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class cartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public cartController(ICartService cartService)
        {
            _cartService = cartService;
        }



        [HttpPost]
        public async Task<IActionResult> AddToCart([FromBody] CartInputModel input)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ResponseModel<string>
                {
                    success = false,
                    message = "Invalid input",
                    data = null
                });
            }

            var userIdClaim = User.FindFirst("id") ?? User.FindFirst("sub") ?? User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            {
                return Unauthorized(new ResponseModel<string>
                {
                    success = false,
                    message = "User not authenticated or invalid claim",
                    data = null
                });
            }

            var book = await _cartService.GetBookById(input.bookId);
            if (book == null)
            {
                return NotFound(new ResponseModel<string>
                {
                    success = false,
                    message = "Book not found",
                    data = null
                });
            }

            // Always add only 1 quantity
            var cartItem = new CartModel
            {
                userId = userId,
                bookId = input.bookId,
                bookQuantity = 1,
                price = book.Price,
                isPurchased = false,
                Book = book
            };

            var result = await _cartService.AddItemToCart(userId, cartItem);

            if (result != null)
            {
                return Ok(new ResponseModel<CartModel>
                {
                    success = true,
                    message = "One item added to cart",
                    data = result
                });
            }

            return BadRequest(new ResponseModel<string>
            {
                success = false,
                message = "Failed to add item to cart",
                data = null
            });
        }





        [HttpGet("item/{cartId}")]
        public IActionResult GetCartItemById(int cartId)
        {
            var item = _cartService.GetCartItemById(cartId);
            if (item == null)
            {
                return NotFound(new ResponseModel<string>
                {
                    success = false,
                    message = "Cart item not found",
                    data = null
                });
            }

            return Ok(new ResponseModel<CartModel>
            {
                success = true,
                message = "Cart item retrieved successfully",
                data = item
            });
        }


        [HttpPut("{cartId}")]
        public async Task<IActionResult> UpdateCartItem(int cartId, [FromBody] CartModel cartItem)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResponseModel<string>
                {
                    success = false,
                    message = "Invalid input",
                    data = null
                });

            
            var book = await _cartService.GetBookById(cartItem.bookId);
            if (book == null)
            {
                return NotFound(new ResponseModel<string>
                {
                    success = false,
                    message = "Book not found",
                    data = null
                });
            }

           
            cartItem.Book = book;

            var result = await _cartService.UpdateCartItem(cartId, cartItem);

            if (result != null)
                return Ok(new ResponseModel<CartModel>
                {
                    success = true,
                    message = "Cart item updated successfully",
                    data = result
                });

            return NotFound(new ResponseModel<string>
            {
                success = false,
                message = "Cart item not found",
                data = null
            });
        }


        [HttpDelete("{cartId}")]
        public async Task<IActionResult> RemoveCartItem(int cartId)
        {
            var result = await _cartService.DeleteCartItem(cartId);

            if (result)
            {
                return Ok(new ResponseModel<string>
                {
                    success = true,
                    message = "Cart item removed successfully",
                    data = null
                });
            }

            return NotFound(new ResponseModel<string>
            {
                success = false,
                message = "Cart item not found",
                data = null
            });
        }



        [HttpPost("purchase/{cartItemId}")]
        public async Task<IActionResult> PurchaseCart(int cartItemId)
        {
            var result = await _cartService.PurchaseCartItem(cartItemId);

            if (result != null)
                return Ok(new ResponseModel<CartModel>
                {
                    success = true,
                    message = "Cart item purchased successfully",
                    data = result
                });

            return NotFound(new ResponseModel<string>
            {
                success = false,
                message = "Cart item not found or already purchased",
                data = null
            });
        }

    }
}
