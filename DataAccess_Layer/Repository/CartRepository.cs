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

        public async Task<CartModel?> AddToCart(int userId, CartModel model)
        {
            var existingCartItem = await _context.Carts
                .Include(c => c.Book)
                .FirstOrDefaultAsync(ci => ci.userId == userId && ci.bookId == model.bookId && !ci.isPurchased);

            if (existingCartItem != null)
            {
                existingCartItem.bookQuantity += 1;
                _context.Carts.Update(existingCartItem);
                await _context.SaveChangesAsync();
              
                return existingCartItem;
            }

            var book = await _context.Books.FindAsync(model.bookId);
            if (book == null)
                return null;

            var newCartItem = new CartModel
            {
                userId = userId,
                bookId = model.bookId,
                bookQuantity = 1,
                price = book.Price,
                isPurchased = false
            };

            _context.Carts.Add(newCartItem);
            await _context.SaveChangesAsync();           
            return newCartItem;
        }

        public List<CartModel> GetCartItems(int userId)
        {
            return _context.Carts
                .Where(c => c.userId == userId && c.isPurchased==false)
                .Include(c => c.Book) 
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
            var item = _context.Carts
                .Include(c => c.Book) // Ensure Book is loaded for price calculation
                .FirstOrDefault(c => c.cartItemId == cartItemId);

            if (item != null)
            {
                if (item.bookQuantity > 1)
                {
                    item.bookQuantity -= 1;
                    item.price = item.Book.Price * item.bookQuantity;
                    _context.Carts.Update(item);
                }
                else
                {
                    _context.Carts.Remove(item);
                }

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
