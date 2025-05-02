// Required usings
using DataAccess_Layer.Interfaces;
using DataAccess_Layer.Models;
using Microsoft.EntityFrameworkCore; // Needed for Include
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess_Layer.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly BookStoreContext _context;

        public CartRepository(BookStoreContext context)
        {
            _context = context;
        }

        public CartModel AddToCart(CartModel cart)
        {
            // Optionally fetch book price
            var book = _context.Books.FirstOrDefault(b => b.Id == cart.bookId);
            if (book != null)
            {
                cart.price = book.Price * cart.bookQuantity; 
            }

            _context.Carts.Add(cart);
            _context.SaveChanges();
            return cart;
        }

        public List<CartModel> GetCartItems(int userId)
        {
            return _context.Carts
                .Where(c => c.userId == userId)
                .Include(c => c.Book) // Eager load book data
                .ToList();
        }

        public CartModel GetCartItemById(int cartItemId)
        {
            return _context.Carts
                .Include(c => c.Book)
                .FirstOrDefault(c => c.cartItemId == cartItemId);
        }

        public CartModel UpdateCart(CartModel cart)
        {
            var book = _context.Books.FirstOrDefault(b => b.Id == cart.bookId);
            if (book != null)
            {
                cart.price = book.Price * cart.bookQuantity;
            }

            _context.Carts.Update(cart);
            _context.SaveChanges();
            return cart;
        }

        public bool DeleteCartItem(int cartItemId)
        {
            var item = _context.Carts.FirstOrDefault(c => c.cartItemId == cartItemId);
            if (item != null)
            {
                _context.Carts.Remove(item);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public CartModel PurchaseCartItem(int cartItemId)
        {
            var item = _context.Carts.FirstOrDefault(c => c.cartItemId == cartItemId);
            if (item != null)
            {
                item.isPurchased = true;
                _context.Carts.Update(item);
                _context.SaveChanges();
                return item;
            }
            return null;
        }

        public BookModel GetBookById(int bookId)
        {
            return _context.Books.FirstOrDefault(b => b.Id == bookId);
        }
    }
}
