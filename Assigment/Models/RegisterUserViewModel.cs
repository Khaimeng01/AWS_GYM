using System;
using System.ComponentModel.DataAnnotations;

namespace Assigment.Models
{
    public class RegisterUserViewModel
    {
        public string Username { get; set; }
        

        [Required(ErrorMessage = "Email  is required")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        

        [Required(ErrorMessage = "PhoneNumber is required")]
        [StringLength(10, ErrorMessage = "Please Enter in 10 numbers only", MinimumLength = 10)]
        public string PhoneNumber { get; set; }
       

        [Required(ErrorMessage = "Address is required")]
        public string address { get; set; }
        

        public DateTime RegisteredDate { get; set; }

        [StringLength(12, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 12)]
        [Required(ErrorMessage = "IC is required")]
        public string Ic { get; set; }
       
       
        [Required(ErrorMessage = "Full Name is required")]
        public string FullName { get; set; }
        

    }
}
