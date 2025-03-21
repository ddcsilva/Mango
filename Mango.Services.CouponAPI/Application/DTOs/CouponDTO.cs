namespace Mango.Services.CouponAPI.Application.DTOs;

public record CouponDTO(int CouponId, string CouponCode, double DiscountAmount, double MinAmount);
