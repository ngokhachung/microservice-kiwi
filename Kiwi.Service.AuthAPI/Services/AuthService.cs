using Kiwi.Service.AuthAPI.Data;
using Kiwi.Service.AuthAPI.Models;
using Kiwi.Service.AuthAPI.Models.DTO;
using Kiwi.Service.AuthAPI.Services.IService;
using Microsoft.AspNetCore.Identity;

namespace Kiwi.Service.AuthAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthService(AppDbContext db, IJwtTokenGenerator jwtTokenGenerator,
    UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _jwtTokenGenerator = jwtTokenGenerator;
            _userManager = userManager;
            _roleManager = roleManager;
        }

      

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            var user = _db.ApplicationUsers.FirstOrDefault(x => x.UserName == loginRequestDto.Username);
            var isValid = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);
            if(user == null || !isValid)
            {
                return new LoginResponseDto()
                {
                    AccessToken = "",
                    User = null
                };
            }
            var roles = await _userManager.GetRolesAsync(user);
            return new LoginResponseDto()
            {
                User = new()
                {
                    Email = user.Email,
                    Id = user.Id,
                    Name = user.Name,
                    PhoneNumber = user.PhoneNumber,
                    Username = user.UserName
                },
                AccessToken = _jwtTokenGenerator.GenerateToken(user, roles)
            };
        }

        public async Task<string> Register(RegistrationRequestModel registrationRequestDto)
        {
            ApplicationUser user = new()
            {
                UserName = registrationRequestDto.Username,
                Email = registrationRequestDto.Email,
                PhoneNumber = registrationRequestDto.PhoneNumber,
                Name = registrationRequestDto.Name,
            };
            try
            {
                var result = await _userManager.CreateAsync(user, registrationRequestDto.Password);
                if (result.Succeeded)
                {
                    await AssignRole(user.Email, registrationRequestDto.Role);
                    return string.Empty;
                }
                else {
                    return result.Errors.FirstOrDefault().Description;
                }
            }
            catch (Exception ex) { 
                return ex.Message;
            }
        }

        private async Task<bool> AssignRole(string email, string roleName)
        {
            var user = _db.ApplicationUsers.FirstOrDefault(u => u.Email.ToLower() == email.ToLower());
            if (user != null)
            {
                if (!_roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
                {
                    // Create new role if doest not exist
                    _roleManager.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult();
                }
                try
                {
                    await _userManager.AddToRoleAsync(user, roleName);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                return true;
            }
            return false;
        }
    }
}
