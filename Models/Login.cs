using System.ComponentModel.DataAnnotations;

namespace AuthService.Models
{
    public class Login
    {
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
