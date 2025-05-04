using DataAccess_Layer.Interfaces;
using DataAccess_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness_Layer.Services
{
    public class OrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public Task<bool> AddOrderAsync(int userId)
        {
            return _orderRepository.PlaceOrderAsync(userId);
        }

        public IEnumerable<OrderModel> GetUserOrders(int userId)
        {
            return _orderRepository.GetOrdersByUserId(userId);
        }

        public OrderModel GetUserOrderById(int userId, int orderId)
        {
            return _orderRepository.GetOrderById(userId, orderId);
        }
    }
}
