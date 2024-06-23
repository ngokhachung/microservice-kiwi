namespace Kiwi.Service.AuthAPI.Models.DTO
{
    public class RegistrationRequestModel
    {
        public string Name { get; set; }
        public string Username  { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}
