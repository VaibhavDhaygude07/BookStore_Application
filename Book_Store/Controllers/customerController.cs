using Bussiness_Layer.Interfaces;
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
    public class customerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public customerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost]
        public IActionResult AddCustomer([FromBody] CustomerModel model)
        {
            var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            model.UserId = userId;

            var result = _customerService.AddCustomerAsync(model);
            return Ok(new { success = true, message = "Customer added successfully", data = result });
        }

        [HttpGet("profile")]
        public IActionResult GetCustomer()
        {
            var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var result = _customerService.GetCustomerByUserIdAsync(userId);

            if (result == null)
                return NotFound(new { success = false, message = "Customer not found" });

            return Ok(new { success = true, message = "Customer retrieved", data = result });
        }

        [HttpPut]
        public IActionResult UpdateCustomer([FromBody] CustomerModel model)
        {
            var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            model.UserId = userId;

            var result = _customerService.UpdateCustomerAsync(model);
            return Ok(new { success = true, message = "Customer updated successfully", data = result });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var success = await _customerService.DeleteCustomerAsync(id);
            if (!success)
                return NotFound(new { success = false, message = "Customer not found" });

            return Ok(new { success = true, message = "Customer deleted successfully" });
        }


    }
}
