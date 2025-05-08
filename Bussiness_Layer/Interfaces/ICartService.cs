using DataAccess_Layer.DTO_s;
using DataAccess_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness_Layer.Interfaces
{
    public interface ICartService
    {
        List<CartModel> GetAllCartItems(int userId);
        CartModel GetCartItemById(int cartId);
        Task<CartModel> AddItemToCart(int userId, CartModel cartModel);
        Task<CartModel> UpdateCartItem(int cartId, CartModel cart);
        Task<bool> DeleteCartItem(int cartItemId);
        Task<CartModel> PurchaseCartItem(int cartItemId);
        Task<BookModel> GetBookById(int bookId);
        //Task<IEnumerable<object>> GetCartItemsByUserIdAsync(int userId);
        //Task<List<CartModel>> GetCartItemsByUserIdAsync(int userId);
    }
}
