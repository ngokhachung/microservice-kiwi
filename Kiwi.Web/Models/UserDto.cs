namespace Kiwi.Service.AuthAPI.Models.DTO
{
    public class UserDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public string Role { get; set; }
    }
}
