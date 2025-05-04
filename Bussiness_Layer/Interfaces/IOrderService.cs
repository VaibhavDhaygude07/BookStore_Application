using DataAccess_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness_Layer.Interfaces
{
    public interface IOrderService
    {
        Task<bool> AddOrderAsync(int userId);
        IEnumerable<OrderModel> GetUserOrders(int userId);
        OrderModel GetUserOrderById(int userId, int orderId);
    }
}
