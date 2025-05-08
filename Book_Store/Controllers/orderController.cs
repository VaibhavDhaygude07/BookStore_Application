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

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var orders = await _orderService.GetPurchasedOrdersAsync(userId);

            if (!orders.Any())
                return NotFound(new { success = false, message = "No orders found." });

            return Ok(new { success = true, data = orders });
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder()
        {
            var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var placedOrders = await _orderService.PlaceOrderAsync(userId);

            if (!placedOrders.Any())
                return BadRequest(new { success = false, message = "No cart items to place an order." });

            return Ok(new { success = true, message = "Order placed successfully.", data = placedOrders });
        }
    }
}


