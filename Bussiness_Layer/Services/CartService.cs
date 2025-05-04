using Bussiness_Layer.Interfaces;
using DataAccess_Layer;
using DataAccess_Layer.DTO_s;
using DataAccess_Layer.Interfaces;
using DataAccess_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness_Layer.Services
{

    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;

        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }


        public List<CartModel> GetAllCartItems(int userId)
        {
            return _cartRepository.GetCartItems(userId);
        }

        public CartModel GetCartItemById(int cartId)
        {
            return _cartRepository.GetCartItemById(cartId);
        }

        public Task<CartModel> AddItemToCart(int userId, CartModel cartModel)
        {
            return _cartRepository.AddToCart(userId, cartModel);
        }

        public Task<bool> DeleteCartItem(int cartItemId)
        {
            return Task.FromResult(_cartRepository.DeleteCartItem(cartItemId));
        }

        public Task<CartModel> UpdateCartItem(int cartId, CartModel cartModel)
        {
            return Task.FromResult(_cartRepository.UpdateCart(cartModel));
        }

        public Task<CartModel> PurchaseCartItem(int cartItemId)
        {
            return Task.FromResult(_cartRepository.PurchaseCartItem(cartItemId));
        }

        public Task<BookModel> GetBookById(int bookId)
        {
            return Task.FromResult(_cartRepository.GetBookById(bookId));
        }

        

    }
}
