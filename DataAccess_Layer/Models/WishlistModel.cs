using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess_Layer.Models
{
    public class WishlistModel
    {
        [Key, Column(Order = 0)]
        [Required(ErrorMessage = "User ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "User ID must be a positive number")]
        public int userId { get; set; }

        [Key, Column(Order = 1)]
        [Required(ErrorMessage = "Book ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Book ID must be a positive number")]
        public int bookId { get; set; }

        [ForeignKey("bookId")]
        public BookModel Book { get; set; }


    }
}
