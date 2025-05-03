using DataAccess_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness_Layer.Interfaces
{
    public interface IWishlistService
    { 
        Task<List<WishlistModel>> GetWishlistByUser(int userId);
        Task<WishlistModel> AddToWishlist(int userId, int bookId);
        Task<bool> RemoveFromWishlist(int userId, int bookId);
       
    }
}
