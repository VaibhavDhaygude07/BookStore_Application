using DataAccess_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess_Layer.Interfaces
{
    public interface IWishlistRepo
    {
        Task<WishlistModel> AddToWishlistAsync(WishlistModel wishlist);
        Task<bool> RemoveFromWishlistAsync(int userId, int bookId);
        Task<List<WishlistModel>> GetWishlistByUserIdAsync(int userId);
    }   
}
