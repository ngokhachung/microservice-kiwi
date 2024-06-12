using Kiwi.Web.Models;

namespace Kiwi.Web.Services.IServices
{
    public interface IBaseService
    {
        Task<ResponseModel?> SendAsync(RequestModel requestDto, bool withBearer = true);
    }
}
