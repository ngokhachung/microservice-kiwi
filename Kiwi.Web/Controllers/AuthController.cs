using Kiwi.Service.AuthAPI.Models.DTO;
using Kiwi.Web.Models;
using Kiwi.Web.Services.IServices;
using Kiwi.Web.Utilites;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Kiwi.Web.Controllers
{
    public class AuthController(IAuthService authService, ITokenProvider tokenProvider) : Controller
    {
        private readonly IAuthService _authService = authService;
        private readonly ITokenProvider _tokenProvider = tokenProvider;

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            var roleList = new List<SelectListItem>()
            {
                new() {Text=SD.RoleAdmin,Value=SD.RoleAdmin},
                new() {Text=SD.RoleCustomer,Value=SD.RoleCustomer},
            };

            ViewBag.RoleList = roleList;
            return View();
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            _tokenProvider.ClearToken();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestModel loginRequestModel)
        {
            if (ModelState.IsValid) 
            {
                var res = await _authService.LoginAsync(loginRequestModel);
                if (res != null && res.IsSuccess)
                {
                    var loginResponse = JsonConvert.DeserializeObject<LoginResponseModel>(Convert.ToString(res.Data));
                    await SignInUser(loginResponse);
                    _tokenProvider.SetToken(loginResponse.AccessToken);
                    TempData["success"] = "Login successfully";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["error"] = res?.Message;
                    return View(loginRequestModel);
                }
            }
            TempData["error"] = "Login fail";
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegistrationRequestModel obj)
        {
            if (ModelState.IsValid)
            {
                var res = await _authService.RegisterAsync(obj);
                if (res != null && res.IsSuccess)
                {
                    TempData["success"] = "Registration Successful";
                    return RedirectToAction(nameof(Login));
                }
                else
                {
                    TempData["error"] = res?.Message;
                }
            }
            var roleList = new List<SelectListItem>()
            {
                new() {Text=SD.RoleAdmin,Value=SD.RoleAdmin},
                new() {Text=SD.RoleCustomer,Value=SD.RoleCustomer},
            };

            ViewBag.RoleList = roleList;
            return View(obj);
        }

    
        private async Task SignInUser(LoginResponseModel loginResponseModel)
        {
            var handler = new JwtSecurityTokenHandler();

            var jwt = handler.ReadJwtToken(loginResponseModel.AccessToken);

            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Email,
                jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value));
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub,
                jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sub).Value));
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Name,
                jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Name).Value));


            identity.AddClaim(new Claim(ClaimTypes.Name,
                jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value));
            identity.AddClaim(new Claim(ClaimTypes.Role,
                jwt.Claims.FirstOrDefault(u => u.Type == "role").Value));

            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }
    }
}
