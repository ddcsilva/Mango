namespace Mango.Web.Features.Coupons.DTOs;

public class CouponDTO
{
    public int CouponId { get; set; }
    public string CouponCode { get; set; } = string.Empty;
    public double DiscountAmount { get; set; }
    public double? MinAmount { get; set; }
}
