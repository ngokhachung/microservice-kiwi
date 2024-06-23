using System.ComponentModel.DataAnnotations;

namespace Kiwi.Service.AuthAPI.Models.DTO
{
    public class RegistrationRequestModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Username  { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Email { get; set; }

        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        public string Role { get; set; } = string.Empty;
    }
}
