using System.ComponentModel.DataAnnotations;

namespace Mango.Services.ProductAPI.Domain.Models;

public class Product
{
    [Key]
    public int ProductId { get; set; }
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;
    public double Price { get; set; }
    public string Description { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
}

