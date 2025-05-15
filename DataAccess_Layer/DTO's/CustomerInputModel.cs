    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace DataAccess_Layer.DTO_s
    {
        public class CustomerInputModel
        {
        

            [Required(ErrorMessage = "Full name is required.")]
            [StringLength(100, MinimumLength = 3, ErrorMessage = "Full name must be between 3 and 100 characters.")]
            public string FullName { get; set; }

            [Required(ErrorMessage = "Phone number is required.")]
            [RegularExpression(@"^[6-9]\d{9}$", ErrorMessage = "Phone number must be a valid 10-digit Indian number starting with 6-9.")]
            public string PhoneNumber { get; set; }

            [Required(ErrorMessage = "Address is required.")]
            [StringLength(250, MinimumLength = 10, ErrorMessage = "Address must be between 10 and 250 characters.")]
            public string Address { get; set; }

            public string? City { get; set; }
            public string? State { get; set; }
  
        }
    }
