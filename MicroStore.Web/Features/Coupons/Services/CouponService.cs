using Microsoft.Extensions.Options;
using MicroStore.Web.Features.Coupons.Interfaces;
using MicroStore.Web.Core.Enums;
using MicroStore.Web.Core.DTOs;
using MicroStore.Web.Features.Coupons.DTOs;
using MicroStore.Web.Core.Base;
using MicroStore.Web.Core.Utilities;

namespace MicroStore.Web.Features.Coupons.Services;

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
    