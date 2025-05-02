using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess_Layer.Models
{
    public class GetAllCardModel<T>
    {
        public int totalPrice { get; set; }

        public List<CartModel> cartItems { get; set; }
    }
}
