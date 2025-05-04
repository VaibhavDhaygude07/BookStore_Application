using DataAccess_Layer.Interfaces;
using DataAccess_Layer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess_Layer.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly BookStoreContext _context;

        public OrderRepository(BookStoreContext context)
        {
            _context = context;
        }

        public async Task<bool> PlaceOrderAsync(int userId)
        {
            var cartItems = _context.Carts
                .Include(c => c.Book)
                .Where(c => c.userId == userId && !c.isPurchased)
                .ToList();

            if (!cartItems.Any())
                return false;

            var totalAmount = cartItems.Sum(c => c.price);

            var order = new OrderModel
            {
                UserId = userId,
                OrderDate = DateTime.Now,
                TotalAmount = totalAmount,
                IsCompleted = true
            };

            _context.Orders.Add(order);

            foreach (var item in cartItems)
            {
                item.isPurchased = true;
                _context.Carts.Update(item);
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public IEnumerable<OrderModel> GetOrdersByUserId(int userId)
        {
            return _context.Orders
                .Where(o => o.UserId == userId)
                .OrderByDescending(o => o.OrderDate)
                .ToList();
        }

        public OrderModel GetOrderById(int userId, int orderId)
        {
            return _context.Orders
                .FirstOrDefault(o => o.UserId == userId && o.OrderId == orderId);
        }
    }
}
