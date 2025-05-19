using DataAccess_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess_Layer.Interfaces
{
    public interface ICartRepository
    {
        Task<CartModel?> AddToCart(int userId, CartModel cart);

        List<CartModel> GetCartItems(int userId);
        CartModel GetCartItemById(int cartItemId);
        CartModel UpdateCart(CartModel cart);
        bool DeleteCartItem(int cartItemId);
       

        CartModel PurchaseCartItem(int cartItemId);
        BookModel GetBookById(int bookId);


    }
}
