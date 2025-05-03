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
    public class WishlistRepository: IWishlistRepo
    {
        private readonly BookStoreContext _context;

        public WishlistRepository(BookStoreContext context)
        {
            _context = context;
        }

        public async Task<WishlistModel> AddToWishlistAsync(WishlistModel wishlist)
        {
            var existing = await _context.Wishlists
                .FirstOrDefaultAsync(w => w.userId == wishlist.userId && w.bookId == wishlist.bookId);

            if (existing != null)
                return null;

            _context.Wishlists.Add(wishlist);
            await _context.SaveChangesAsync();

            // Reload with book details
            return await _context.Wishlists
                .Include(w => w.Book)
                .FirstOrDefaultAsync(w => w.userId == wishlist.userId && w.bookId == wishlist.bookId);
        }


        public async Task<bool> RemoveFromWishlistAsync(int userId, int bookId)
        {
            var item = await _context.Wishlists
                .FirstOrDefaultAsync(w => w.userId == userId && w.bookId == bookId);

            if (item == null)
                return false;

            _context.Wishlists.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<WishlistModel>> GetWishlistByUserIdAsync(int userId)
        {
            return await _context.Wishlists
                .Include(w => w.Book) // Optional: if you want full book info
                .Where(w => w.userId == userId)
                .ToListAsync();
        }
    }
}
