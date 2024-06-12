using Kiwi.Web.Models;
using Kiwi.Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Kiwi.Web.Controllers
{
    public class CouponController(ICouponService couponService) : Controller
    {
        private readonly ICouponService _couponService = couponService;
        public async Task<IActionResult> CouponIndex()
        {
            List<CouponDto>? list = [];

            ResponseModel? response = await _couponService.GetAllCouponsAsync();

            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<CouponDto>>(Convert.ToString(response.Data));
            }
            else
            {
                TempData["error"] = response?.Message;
            }

            return View(list);
        }

        [HttpGet]
        public IActionResult CouponCreate()
        {
            return View("CouponCreate");
        }

        [HttpPost]
        public async Task<IActionResult> CouponCreate(CouponDto coupon)
        {
            if (ModelState.IsValid)
            {
                var res = await _couponService.CreateCouponsAsync(coupon);
                if (res != null && res.IsSuccess)
                {
                    TempData["success"] = "Coupon created successfully";
                    return RedirectToAction("CouponIndex");
                }
                else
                {
                    TempData["error"] = res?.Message;
                }
            }
            return View(coupon);
        }

        [HttpGet]
        public async Task<IActionResult> CouponDelete(int couponId)
        {
            ResponseModel? response = await _couponService.GetCouponByIdAsync(couponId);

            if (response != null && response.IsSuccess)
            {
                CouponDto? model = JsonConvert.DeserializeObject<CouponDto>(Convert.ToString(response.Data));
                return View(model);
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CouponDelete(CouponDto couponDto)
        {
            ResponseModel? response = await _couponService.DeleteCouponsAsync(couponDto.CouponId);

            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Coupon deleted successfully";
                return RedirectToAction("CouponIndex");
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return View(couponDto);
        }
    }
}
