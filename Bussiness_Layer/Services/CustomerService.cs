using Bussiness_Layer.Interfaces;
using DataAccess_Layer.Interfaces;
using DataAccess_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness_Layer.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<CustomerModel> AddCustomerAsync(CustomerModel customer)
        {
            return await _customerRepository.AddCustomerAsync(customer);
        }

        public async Task<CustomerModel> GetCustomerByUserIdAsync(int userId)
        {
            return await _customerRepository.GetCustomerByUserIdAsync(userId);
        }

        public async Task<List<CustomerModel>> GetAllCustomersAsync()
        {
            return await _customerRepository.GetAllCustomersAsync();
        }

        public async Task<CustomerModel> UpdateCustomerAsync(CustomerModel customer)
        {
            return await _customerRepository.UpdateCustomerAsync(customer);
        }

        public async Task<bool> DeleteCustomerAsync(int customerId)
        {
            return await _customerRepository.DeleteCustomerAsync(customerId);
        }

        public async Task<CustomerModel> GetCustomerByIdAsync(int customerId)
        {
            return await _customerRepository.GetCustomerByIdAsync(customerId);
        }

    }
}
