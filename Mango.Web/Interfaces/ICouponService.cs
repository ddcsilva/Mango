using Mango.Web.DTOs;

namespace Mango.Web.Interfaces;

public interface ICouponService
{
    Task<ResponseDTO?> GetCouponByCodeAsync(string couponCode);
    Task<ResponseDTO?> GetAllCouponsAsync();
    Task<ResponseDTO?> GetCouponByIdAsync(int couponId);
    Task<ResponseDTO?> CreateCouponAsync(CouponDTO couponDTO);
    Task<ResponseDTO?> UpdateCouponAsync(CouponDTO couponDTO);
    Task<ResponseDTO?> DeleteCouponAsync(int couponId);
}
