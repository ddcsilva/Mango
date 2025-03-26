using Mango.Web.Core.DTOs;
using Mango.Web.Core.Enums;
using Mango.Web.Core.Base;
using Mango.Web.Core.Utilities;
using Mango.Web.Features.Coupons.Interfaces;
using Microsoft.Extensions.Options;
using Mango.Web.Features.Coupons.DTOs;

namespace Mango.Web.Features.Coupons.Services;

public class CouponService(IBaseService baseService, IOptions<ServiceUrls> serviceUrls) : ICouponService
{
    private readonly string _couponApiBase = serviceUrls.Value.CouponApiBase;

    public async Task<ResponseDTO> GetAllCouponsAsync()
    {
        return await baseService.SendAsync(new RequestDTO
        {
            ApiType = ApiType.GET,
            Url = $"{_couponApiBase}/api/coupon"
        });
    }

    public async Task<ResponseDTO> GetCouponByCodeAsync(string couponCode)
    {
        if (string.IsNullOrWhiteSpace(couponCode))
        {
            return new ResponseDTO{ Result = null, IsSuccess = false, Message = "Código do cupom não pode estar vazio." };
        }

        return await baseService.SendAsync(new RequestDTO
        {
            ApiType = ApiType.GET,
            Url = $"{_couponApiBase}/api/coupon/code/{Uri.EscapeDataString(couponCode)}"
        });
    }

    public async Task<ResponseDTO> GetCouponByIdAsync(int couponId)
    {
        if (couponId <= 0)
        {
            return new ResponseDTO { Result = null, IsSuccess = false, Message = "ID do cupom inválido." };
        }

        var response = await baseService.SendAsync(new RequestDTO
        {
            ApiType = ApiType.GET,
            Url = $"{_couponApiBase}/api/coupon/{couponId}"
        });

        if (!response.IsSuccess)
        {
            response.Message = $"Erro ao buscar cupom {couponId}: {response.Message}";
        }

        return response;
    }

    public async Task<ResponseDTO> CreateCouponAsync(CouponDTO couponDTO)
    {
        return await baseService.SendAsync(new RequestDTO
        {
            ApiType = ApiType.POST,
            Url = $"{_couponApiBase}/api/coupon",
            Data = couponDTO
        });
    }

    public async Task<ResponseDTO> UpdateCouponAsync(CouponDTO couponDTO)
    {
        return await baseService.SendAsync(new RequestDTO
        {
            ApiType = ApiType.PUT,
            Url = $"{_couponApiBase}/api/coupon",
            Data = couponDTO
        });
    }

    public async Task<ResponseDTO> DeleteCouponAsync(int couponId)
    {
        if (couponId <= 0)
        {
            return new ResponseDTO { Result = null, IsSuccess = false, Message = "ID do cupom inválido." };
        }

        var response = await baseService.SendAsync(new RequestDTO
        {
            ApiType = ApiType.DELETE,
            Url = $"{_couponApiBase}/api/coupon/{couponId}"
        });

        if (!response.IsSuccess)
        {
            response.Message = $"Erro ao excluir cupom {couponId}: {response.Message}";
        }

        return response;
    }
}
    