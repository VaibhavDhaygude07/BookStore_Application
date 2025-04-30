using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess_Layer.DTO_s
{
    public class ResetPasswordDto
    {
        [Required(ErrorMessage = "Email is required.")]
        public string EmailId { get; set; }

        [Required(ErrorMessage = "New password is required.")]
        public string NewPassword { get; set; }

        [Compare("NewPassword", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }


        [Required(ErrorMessage = "Token is required.")]
        public string Token { get; set; }
    }
}
