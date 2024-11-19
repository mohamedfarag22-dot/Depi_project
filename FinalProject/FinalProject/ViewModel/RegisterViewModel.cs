using System.ComponentModel.DataAnnotations;

namespace FinalProject.ViewModel
{
    public class RegisterVeiwModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The passwords don't match")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Role { get; set; }

        public string? Job { get; set; }
        public string UserName { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }
    }
}
