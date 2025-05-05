using Bussiness_Layer.Interfaces;
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
    public class customerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public customerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost]
        public async Task<IActionResult> AddCustomer([FromBody] CustomerInputModel input)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { success = false, message = "Invalid input", errors = ModelState });

            var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var customer = new CustomerModel
            {
                UserId = userId,
                FullName = input.FullName,
                PhoneNumber = input.PhoneNumber,
                Address = input.Address,
                City = input.City,
                State = input.State
            };

            var result = await _customerService.AddCustomerAsync(customer);
            return Ok(new { success = true, message = "Customer added", data = result });
        }


        [HttpGet]
        public async Task<IActionResult> GetCustomer()
        {
            var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var result = await _customerService.GetCustomerByUserIdAsync(userId);

            if (result == null)
                return NotFound(new { success = false, message = "Customer not found" });

            return Ok(new { success = true, message = "Customer found", data = result });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCustomer([FromBody] CustomerInputModel input)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { success = false, message = "Invalid input", errors = ModelState });

            var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var customerModel = new CustomerModel
            {
                CustomerId = input.CustomerId,
                FullName = input.FullName,
                PhoneNumber = input.PhoneNumber,
                Address = input.Address,
                City = input.City,
                State = input.State,
                UserId = userId
            };

            var result = await _customerService.UpdateCustomerAsync(customerModel);
            return Ok(new { success = true, message = "Customer updated", data = result });
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
