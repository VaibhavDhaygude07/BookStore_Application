using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess_Layer.DTO_s
{
    public class CartInputModel
    {
        [ForeignKey("Book")]
        [Required(ErrorMessage = "Book ID is required")]
        public int bookId { get; set; }

        [Required(ErrorMessage = "Book quantity is required")]
        public int bookQuantity { get; set; }
       
    }
}
