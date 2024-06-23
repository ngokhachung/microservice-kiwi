using Kiwi.Service.AuthAPI.Models.DTO;

namespace Kiwi.Service.AuthAPI.Services.IService
{
    public interface IAuthService
    {
        public Task<string> Register(RegistrationRequestModel registrationRequestDto);
        public Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);

    }

}
