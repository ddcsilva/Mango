using Mango.Web.Core.DTOs;
using Mango.Web.Features.Coupons.DTOs;

namespace Mango.Web.Features.Coupons.Interfaces;

public interface ICouponService
{
    Task<ResponseDTO> GetCouponByCodeAsync(string couponCode);
    Task<ResponseDTO> GetAllCouponsAsync();
    Task<ResponseDTO> GetCouponByIdAsync(int couponId);
    Task<ResponseDTO> CreateCouponAsync(CouponDTO couponDTO);
    Task<ResponseDTO> UpdateCouponAsync(CouponDTO couponDTO);
    Task<ResponseDTO> DeleteCouponAsync(int couponId);
}
