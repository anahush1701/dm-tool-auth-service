using Microsoft.AspNetCore.Identity;

namespace AuthService.Models
{
    public class User : IdentityUser
    {
        public Guid UserId { get; set; }
    }
}
 