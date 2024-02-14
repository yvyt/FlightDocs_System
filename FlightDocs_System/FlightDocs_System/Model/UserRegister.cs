using System.ComponentModel.DataAnnotations;

namespace FlightDocs_System.Model
{
    public class UserRegister
    {
        [Required]
        [StringLength(50)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 8)]
        public string Password { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 8)]
        public string ConfirmPassword { get; set; }

    }
}
