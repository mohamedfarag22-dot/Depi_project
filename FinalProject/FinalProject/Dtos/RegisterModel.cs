using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Dtos
{
    public class RegisterModel
    {
        [Required, StringLength(100)]
        public string Name { get; set; }

        [Required, StringLength(50)]
        public string UserName { get; set; }
        
        [Required, StringLength(250)]
        public string Address { get; set; }

        [Required, EmailAddress ,StringLength(128)]
        public string Email { get; set; }

        [Required, PasswordPropertyText,StringLength(256, MinimumLength = 6)]
        public string Password { get; set; }

        [Required, StringLength(50)]
        public string Role { get; set; }

        [Required, Phone ,StringLength(128)]

        public string Phone { get; set; }

        [Required,StringLength(128)]

        public string Job { get; set; }

    }
}
