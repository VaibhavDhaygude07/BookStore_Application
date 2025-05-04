using DataAccess_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess_Layer.Repository
{
    public class CustomerRepository
    {
        private readonly BookStoreContext _context;

        public CustomerRepository(BookStoreContext context)
        {
            _context = context;
        }

        public CustomerModel AddCustomer(CustomerModel customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
            return customer;
        }

        public CustomerModel GetCustomerByUserId(int userId)
        {
            return _context.Customers.FirstOrDefault(c => c.UserId == userId);
        }

        public List<CustomerModel> GetAllCustomers()
        {
            return _context.Customers.ToList();
        }

        public CustomerModel UpdateCustomer(CustomerModel customer)
        {
            _context.Customers.Update(customer);
            _context.SaveChanges();
            return customer;
        }

        public bool DeleteCustomer(int customerId)
        {
            var customer = _context.Customers.FirstOrDefault(c => c.CustomerId == customerId);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
