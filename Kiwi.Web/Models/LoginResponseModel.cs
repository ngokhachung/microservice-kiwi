namespace Kiwi.Service.AuthAPI.Models.DTO
{
    public class LoginResponseModel
    {
        public UserDto User { get; set; }
        public string AccessToken { get; set; }
    }
}
