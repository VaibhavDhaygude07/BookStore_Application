using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess_Layer.DTO_s
{
    public class ResponseModel<T>
    {
        [Required]
        public bool success { get; set; }

        [Required(ErrorMessage = "Message is required.")]
        public string message { get; set; }

        [Required(ErrorMessage = "Data cannot be null.")]
        public T data { get; set; }
    }
}
