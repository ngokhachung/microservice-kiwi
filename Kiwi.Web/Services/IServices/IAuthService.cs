using Kiwi.Service.AuthAPI.Models.DTO;
using Kiwi.Web.Models;

namespace Kiwi.Web.Services.IServices
{
    public interface IAuthService
    {
        public Task<ResponseModel?> LoginAsync(LoginRequestModel loginRequest);

        public Task<ResponseModel?> RegisterAsync(RegistrationRequestModel registrationRequestModel);
        //public Task<ResponseModel?> AssignRoleAsync(RegistrationRequestModel registrationRequestModel);

    }
}
