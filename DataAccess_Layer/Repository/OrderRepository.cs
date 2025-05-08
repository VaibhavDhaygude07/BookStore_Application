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

        public async Task<List<CartModel>> GetPurchasedOrdersAsync(int userId)
        {
            return await _context.Carts
                .Include(c => c.Book)
                .Where(c => c.userId == userId && c.isPurchased == true)
                .ToListAsync();
        }

        public async Task<List<CartModel>> PlaceOrderAsync(int userId)
        {
            var cartItems = await _context.Carts
                .Where(c => c.userId == userId && !c.isPurchased)
                .ToListAsync();

            if (cartItems == null || !cartItems.Any())
                return new List<CartModel>();

            foreach (var item in cartItems)
            {
                item.isPurchased = true;
            }

            await _context.SaveChangesAsync();
            return cartItems;
        }
    }
}
