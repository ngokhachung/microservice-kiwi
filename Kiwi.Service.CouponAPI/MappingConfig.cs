using AutoMapper;
using Kiwi.Service.CouponAPI.Models;
using Kiwi.Service.CouponAPI.Models.Dto;

namespace Kiwi.Service.CouponAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<CouponDto, Coupon>();
                config.CreateMap<Coupon, CouponDto>();
            });
            return mappingConfig;
        }
    }
}
