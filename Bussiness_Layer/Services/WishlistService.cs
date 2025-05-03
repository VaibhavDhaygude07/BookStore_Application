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
    public class WishlistService : IWishlistService
    {
        private readonly IWishlistRepo _repo;

        public WishlistService(IWishlistRepo repo)
        {
            _repo = repo;
        }

        public async Task<WishlistModel> AddToWishlist(int userId, int bookId)
        {
            if (userId <= 0 || bookId <= 0)
                return null;

            var wishlistItem = new WishlistModel
            {
                userId = userId,
                bookId = bookId
            };

            return await _repo.AddToWishlistAsync(wishlistItem);
        }

        public async Task<bool> RemoveFromWishlist(int userId, int bookId)
        {
            if (userId <= 0 || bookId <= 0)
                return false;

            return await _repo.RemoveFromWishlistAsync(userId, bookId);
        }

        public async Task<List<WishlistModel>> GetWishlistByUser(int userId)
        {
            if (userId <= 0)
                return new List<WishlistModel>();

            return await _repo.GetWishlistByUserIdAsync(userId);
        }
    }
}

