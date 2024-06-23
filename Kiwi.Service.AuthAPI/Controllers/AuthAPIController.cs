using Kiwi.Service.AuthAPI.Models.DTO;
using Kiwi.Service.AuthAPI.Services.IService;
using Microsoft.AspNetCore.Mvc;

namespace Kiwi.Service.AuthAPI.Controllers
{
    [Route("api/auth")]
    public class AuthAPIController(IAuthService authService) : Controller
    {
        private readonly IAuthService _authService = authService;
        private ResponseDto _responseDto = new();

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestModel registrationRequestDto)
        {
         
            var result = await _authService.Register(registrationRequestDto);
            if (!string.IsNullOrEmpty(result)) 
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = result;
                return BadRequest(_responseDto);
            }
            return Ok(_responseDto);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto) 
        {
            var result = await _authService.Login(loginRequestDto);
            if (result.User == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Username or password incorrect!";
                return BadRequest(_responseDto);
            }
            _responseDto.Data = result;
            return Ok(_responseDto);
        }

        //[HttpPost]
        //[Route("assignRole")]
        //public async Task<IActionResult> AssignRole([FromBody] RegistrationRequestModel registrationRequestDto)
        //{
        //    var result = await _authService.AssignRole(registrationRequestDto.Email, registrationRequestDto.Role);
        //    if (!result)
        //    {
        //        _responseDto.IsSuccess = false;
        //        _responseDto.Message = "Username or password incorrect!";
        //        return BadRequest(_responseDto);
        //    }
        //    return Ok();
        //}
    }
}
