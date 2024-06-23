using Kiwi.Service.AuthAPI.Models.DTO;
using Kiwi.Web.Models;
using Kiwi.Web.Services.IServices;
using Kiwi.Web.Utilites;

namespace Kiwi.Web.Services
{
	public class AuthService(IBaseService baseService) : IAuthService
    {
        private readonly IBaseService _baseService = baseService;

        //public async Task<ResponseModel?> AssignRoleAsync(RegistrationRequestModel registrationRequestModel)
        //{
        //    return await _baseService.SendAsync(new RequestModel()
        //    {
        //        ApiType = SD.ApiType.POST,
        //        Data = registrationRequestModel,
        //        Url = SD.AuthAPIBase + "/api/auth/AssignRole"
        //    });
        //}

        public async Task<ResponseModel?> LoginAsync(LoginRequestModel loginRequest)
        {
            return await _baseService.SendAsync(new RequestModel()
            {
                ApiType = SD.ApiType.POST,
                Data = loginRequest,
                Url = SD.AuthAPIBase + "/api/auth/login"
            });
        }

        public async Task<ResponseModel?> RegisterAsync(RegistrationRequestModel registrationRequestModel)
        {
            return await _baseService.SendAsync(new RequestModel()
            {
                ApiType = SD.ApiType.POST,
                Data = registrationRequestModel,
                Url = SD.AuthAPIBase + "/api/auth/register"
            });
        }
    }
}
