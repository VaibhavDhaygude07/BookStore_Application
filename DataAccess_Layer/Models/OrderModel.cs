using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess_Layer.Models
{
    public class OrderModel
    {
        [Key]
        public int OrderId { get; set; }

        [Required]
        public int UserId { get; set; } 

        public DateTime OrderDate { get; set; }

        public decimal TotalAmount { get; set; }

        public bool IsCompleted { get; set; } = false;

        // Navigation
        [ForeignKey("UserId")]
        public virtual UserModel User { get; set; }
    }
}
