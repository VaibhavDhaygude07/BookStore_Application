using DataAccess_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess_Layer.Interfaces
{
    public interface IOrderRepository
    {
        Task<bool> PlaceOrderAsync(int userId);
        IEnumerable<OrderModel> GetOrdersByUserId(int userId);
        OrderModel GetOrderById(int userId, int orderId);
    }
}
