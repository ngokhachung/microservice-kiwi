using Kiwi.Service.AuthAPI.Models;

namespace Kiwi.Service.AuthAPI.Services.IService
{
    public interface IJwtTokenGenerator
    {
        public string GenerateToken(ApplicationUser user, IEnumerable<string> roles);
    }
}
