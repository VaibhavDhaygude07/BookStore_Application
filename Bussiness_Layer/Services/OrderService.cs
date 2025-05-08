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
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<List<CartModel>> GetPurchasedOrdersAsync(int userId)
        {
            return await _orderRepository.GetPurchasedOrdersAsync(userId);
        }

        public async Task<List<CartModel>> PlaceOrderAsync(int userId)
        {
            return await _orderRepository.PlaceOrderAsync(userId);
        }
    }
}
