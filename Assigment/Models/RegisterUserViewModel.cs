using System;
using System.ComponentModel.DataAnnotations;

namespace Assigment.Models
{
    public class RegisterUserViewModel
    {
     
        public string Username { get; set; }
        [Required(ErrorMessage = "Category is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Category is required")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Category is required")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Category is required")]
        public string address { get; set; }
        [Required(ErrorMessage = "Category is required")]
        public DateTime RegisteredDate { get; set; }
        [Required(ErrorMessage = "Category is required")]
        public string Ic { get; set; }

        public string FullName { get; set; }

    }
}
