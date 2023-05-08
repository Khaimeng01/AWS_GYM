using System;
using System.ComponentModel.DataAnnotations;

namespace Assigment.Models
{
    public class editUserViewModel
    {

        public string Id { get; set; }

        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email  is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "PhoneNumber is required")]
        [StringLength(10, ErrorMessage = "Please Enter in 10 numbers only", MinimumLength = 10)]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string address { get; set; }


        public DateTime RegisteredDate { get; set; }

        [StringLength(12, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 12)]
        public string Ic { get; set; }

        public string FullName { get; set;}


    }
}
