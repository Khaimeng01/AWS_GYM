using System.ComponentModel.DataAnnotations;

namespace Assigment.Models
{
    public class EditAdminModel
    {
        [Required(ErrorMessage = "ID is required")]
        public string Id { get; set; }
        [Required(ErrorMessage = "FullName is required")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@admin\.com\.my$", ErrorMessage = "Email must end with @admin.com.my")]
        public string Email { get; set; }

    }
}
