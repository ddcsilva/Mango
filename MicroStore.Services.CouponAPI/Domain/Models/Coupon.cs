using System.ComponentModel.DataAnnotations;

namespace MicroStore.Services.CouponAPI.Domain.Models;

public class Coupon
{
    public int CouponId { get; set; }
    public string CouponCode { get; set; } = string.Empty;
    public double DiscountAmount { get; set; }
    public double MinAmount { get; set; }
}
