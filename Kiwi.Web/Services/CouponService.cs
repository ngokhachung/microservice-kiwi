using Kiwi.Web.Models;
using Kiwi.Web.Services.IServices;
using Kiwi.Web.Utilites;

namespace Kiwi.Web.Services
{
    public class CouponService(IBaseService baseService) : ICouponService
    {
        private readonly IBaseService _baseService = baseService;

        public async Task<ResponseModel?> CreateCouponsAsync(CouponDto couponDto)
        {
            return await _baseService.SendAsync(new RequestModel()
            {
                ApiType = SD.ApiType.POST,
                Data = couponDto,
                Url = SD.CouponAPIBase + "/api/coupon/create"
            });
        }

        public async Task<ResponseModel?> DeleteCouponsAsync(int id)
        {
            return await _baseService.SendAsync(new RequestModel()
            {
                ApiType = SD.ApiType.DELETE,
                Url = SD.CouponAPIBase + "/api/coupon/" + id
            });
        }

        public async Task<ResponseModel?> GetAllCouponsAsync()
        {
            return await _baseService.SendAsync(new RequestModel()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.CouponAPIBase + "/api/coupon"
            });
        }

        public async Task<ResponseModel?> GetCouponAsync(string couponCode)
        {
            return await _baseService.SendAsync(new RequestModel()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.CouponAPIBase + "/api/coupon/GetByCode/" + couponCode
            });
        }

        public async Task<ResponseModel?> GetCouponByIdAsync(int id)
        {
            return await _baseService.SendAsync(new RequestModel()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.CouponAPIBase + "/api/coupon/" + id
            });
        }

        public async Task<ResponseModel?> UpdateCouponsAsync(CouponDto couponDto)
        {
            return await _baseService.SendAsync(new RequestModel()
            {
                ApiType = SD.ApiType.PUT,
                Data = couponDto,
                Url = SD.CouponAPIBase + "/api/coupon"
            });
        }
    }
}
