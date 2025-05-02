using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess_Layer.Models
{
    public class BookModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int DiscountPrice { get; set; }
        public string BookImage { get; set; }
        public string AdminUserId { get; set; }
        public string BookName { get; set; }
        public string Author { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public DateTime CreatedAtDate { get; set; }
        public DateTime? UpdatedAtDate { get; set; }
      

    }
}
