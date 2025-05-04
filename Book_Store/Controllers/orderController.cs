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
    public class orderController : ControllerBase
    {

        private readonly IOrderService _orderService;

        public orderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("place")]
        public async Task<IActionResult> PlaceOrderAsync()
        {
            try
            {
                var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var result = await _orderService.AddOrderAsync(userId);

                if (!result)
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "No items in cart or failed to place order."
                    });
                }

                return Ok(new
                {
                    success = true,
                    message = "Order placed successfully."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult GetAllOrders()
        {
            try
            {
                var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var orders = _orderService.GetUserOrders(userId);

                return Ok(new
                {
                    success = true,
                    message = "Orders retrieved successfully.",
                    data = orders
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpGet("{orderId}")]
        public IActionResult GetOrderById(int orderId)
        {
            try
            {
                var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var order = _orderService.GetUserOrderById(userId, orderId);

                if (order == null)
                {
                    return NotFound(new { success = false, message = "Order not found." });
                }

                return Ok(new
                {
                    success = true,
                    message = "Order retrieved successfully.",
                    data = order
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
    }
        
}
