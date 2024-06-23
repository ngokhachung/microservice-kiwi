namespace Kiwi.Service.AuthAPI.Models.DTO
{
    public class LoginResponseDto
    {
        public UserDto User { get; set; }
        public string AccessToken { get; set; }
    }
}
