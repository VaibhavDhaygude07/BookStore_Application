using DataAccess_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness_Layer.Interfaces
{
    public interface ICustomerService
    {
        Task<CustomerModel> AddCustomerAsync(CustomerModel customer);
        Task<CustomerModel> GetCustomerByUserIdAsync(int userId);
        Task<List<CustomerModel>> GetAllCustomersAsync();
        Task<CustomerModel> UpdateCustomerAsync(CustomerModel customer);
        Task<bool> DeleteCustomerAsync(int customerId);
    }
}
