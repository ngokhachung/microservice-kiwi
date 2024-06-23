using Microsoft.AspNetCore.Identity;

namespace Kiwi.Service.AuthAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? Name { get; set; }
    }
}
