using System.ComponentModel.DataAnnotations;

namespace Mango.Services.CouponAPI.Domain.Models;

public class Coupon
{
    [Key]
    public int CouponId { get; set; }
    [Required]
    [MaxLength(20, ErrorMessage = "O código do cupom não pode ter mais de 20 caracteres.")]
    public string CouponCode { get; set; } = string.Empty;
    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "O desconto deve ser positivo.")]
    public double DiscountAmount { get; set; }
    [Range(0, double.MaxValue, ErrorMessage = "O valor mínimo não pode ser negativo.")]
    public double MinAmount { get; set; }
}
