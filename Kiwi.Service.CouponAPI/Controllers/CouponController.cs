﻿using AutoMapper;
using Kiwi.Service.CouponAPI.Data;
using Kiwi.Service.CouponAPI.Models;
using Kiwi.Service.CouponAPI.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Kiwi.Service.CouponAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponController(AppDbContext appDbContext, IMapper mapper) : Controller
    {
        private AppDbContext _appDbContext = appDbContext;
        private IMapper _mapper = mapper;

		[HttpGet]
        public ResponseDto<IEnumerable<Coupon>> GetAll()
        {
            var res = _appDbContext.Coupons.ToList();
            if (res.Count == 0)
                return ResponseDto<IEnumerable<Coupon>>.Failure("No data");

            return ResponseDto<IEnumerable<Coupon>>.Success(_mapper.Map<IEnumerable<Coupon>>(res));
        }

        [HttpGet]
        [Route("{id:int}")]
        public ResponseDto<Coupon> GetById(int id)
        {
            var res = _appDbContext.Coupons.FirstOrDefault(x => x.CouponId == id);

            if (res == null)
                return ResponseDto<Coupon>.Failure("No data");

            return ResponseDto<Coupon>.Success(_mapper.Map<Coupon>(res));
        }


        [HttpGet]
        [Route("getByCode/{code}")]
        public ResponseDto<Coupon> GetByCode(string code)
        {
            var res = _appDbContext.Coupons.FirstOrDefault(x => x.CouponCode == code);

            if (res == null)
                return ResponseDto<Coupon>.Failure("No data");

            return ResponseDto<Coupon>.Success(_mapper.Map<Coupon>(res));
        }

        [HttpPost]
        [Route("create")]
        public ResponseDto<Coupon> Create([FromBody] CouponDto coupon)
        {
            try
            {
                Coupon obj = _mapper.Map<Coupon>(coupon);
                _appDbContext.Coupons.Add(obj);
                _appDbContext.SaveChanges();
                return ResponseDto<Coupon>.Success(obj);
            }
            catch (Exception ex)
            {
                return ResponseDto<Coupon>.Failure(ex.Message.ToString());
            }
        }

        [HttpPut]
        [Route("update")]
        public ResponseDto<Coupon> Update([FromBody] CouponDto coupon)
        {
            try
            {
                Coupon obj = _mapper.Map<Coupon>(coupon);
                _appDbContext.Coupons.Add(obj);
                _appDbContext.SaveChanges();
                return ResponseDto<Coupon>.Success(obj);
            }
            catch (Exception ex)
            {
                return ResponseDto<Coupon>.Failure(ex.Message.ToString());
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public ResponseDto<Coupon> Delete(int id)
        {
            try
            {
                Coupon obj = _appDbContext.Coupons.First(u => u.CouponId == id);
                _appDbContext.Coupons.Remove(obj);
                _appDbContext.SaveChanges();
                return ResponseDto<Coupon>.Success(obj);
            }
            catch (Exception ex)
            {
                return ResponseDto<Coupon>.Failure(ex.Message.ToString());
            }
        }
    }
}
