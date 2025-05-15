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

        Task<List<CartModel>> GetPurchasedOrdersAsync(int userId);
        Task<List<CartModel>> PlaceOrderAsync(int userId);

    }
}
