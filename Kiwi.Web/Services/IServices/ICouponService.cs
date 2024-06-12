using Kiwi.Web.Models;

namespace Kiwi.Web.Services.IServices
{
    public interface ICouponService
    {
        Task<ResponseModel?> GetCouponAsync(string couponCode);
        Task<ResponseModel?> GetAllCouponsAsync();
        Task<ResponseModel?> GetCouponByIdAsync(int id);
        Task<ResponseModel?> CreateCouponsAsync(CouponDto couponDto);
        Task<ResponseModel?> UpdateCouponsAsync(CouponDto couponDto);
        Task<ResponseModel?> DeleteCouponsAsync(int id);
    }
}
