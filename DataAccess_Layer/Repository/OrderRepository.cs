using DataAccess_Layer.Interfaces;
using DataAccess_Layer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var userExists = await _context.Users.AnyAsync(u => u.Id == userId);
            if (!userExists)
            {
                throw new Exception("User not found.");
            }

            var purchasedOrders = await _context.Carts
                .Include(c => c.Book)
                .Where(c => c.userId == userId && c.isPurchased)
                .ToListAsync();

            if (purchasedOrders == null || !purchasedOrders.Any())
            {
                throw new Exception("No purchased orders found.");
            }

            return purchasedOrders;
        }

        public async Task<List<CartModel>> PlaceOrderAsync(int userId)
        {
            var cartItems = await _context.Carts
                .Include(c => c.Book)
                .Where(c => c.userId == userId && !c.isPurchased)
                .ToListAsync();

            if (cartItems == null || !cartItems.Any())
            {
                return new List<CartModel>();
            }

            foreach (var item in cartItems)
            {
                var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == item.bookId);
                if (book == null)
                {
                    throw new Exception($"Book with ID {item.bookId} not found.");
                }

                if (book.Quantity < item.bookQuantity)
                {
                    throw new Exception($"Not enough stock for book ID {item.bookId}.");
                }

               
                book.Quantity -= item.bookQuantity;
                _context.Books.Update(book);

            
                item.isPurchased = true;
                //item.purchaseDate = DateTime.Now; 
            }

            await _context.SaveChangesAsync();
            return cartItems;
        }
    }
}
