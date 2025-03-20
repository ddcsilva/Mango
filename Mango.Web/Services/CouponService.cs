using Mango.Web.DTOs;
using Mango.Web.Enums;
using Mango.Web.Interfaces;
using Mango.Web.Utilities;
using Microsoft.Extensions.Options;

namespace Mango.Web.Services;

public class CouponService(IBaseService baseService, IOptions<ServiceUrls> serviceUrls) : ICouponService
{
    private readonly IBaseService _baseService = baseService;
    private readonly string _couponApiBase = serviceUrls.Value.CouponApiBase;

    public async Task<ResponseDTO?> GetAllCouponsAsync()
    {
        return await _baseService.SendAsync(new RequestDTO
        {
            ApiType = ApiType.GET,
            Url = $"{_couponApiBase}/api/coupon"
        });
    }

    public async Task<ResponseDTO?> GetCouponByCodeAsync(string couponCode)
    {
        return await _baseService.SendAsync(new RequestDTO
        {
            ApiType = ApiType.GET,
            Url = $"{_couponApiBase}/api/coupon/code/{couponCode}"
        });
    }

    public async Task<ResponseDTO?> GetCouponByIdAsync(int couponId)
    {
        return await _baseService.SendAsync(new RequestDTO
        {
            ApiType = ApiType.GET,
            Url = $"{_couponApiBase}/api/coupon/{couponId}"
        });
    }

    public async Task<ResponseDTO?> CreateCouponAsync(CouponDTO couponDTO)
    {
        return await _baseService.SendAsync(new RequestDTO
        {
            ApiType = ApiType.POST,
            Url = $"{_couponApiBase}/api/coupon",
            Data = couponDTO
        });
    }

    public async Task<ResponseDTO?> UpdateCouponAsync(CouponDTO couponDTO)
    {
        return await _baseService.SendAsync(new RequestDTO
        {
            ApiType = ApiType.PUT,
            Url = $"{_couponApiBase}/api/coupon",
            Data = couponDTO
        });
    }

    public async Task<ResponseDTO?> DeleteCouponAsync(int couponId)
    {
        return await _baseService.SendAsync(new RequestDTO
        {
            ApiType = ApiType.DELETE,
            Url = $"{_couponApiBase}/api/coupon/{couponId}"
        });
    }
}
    