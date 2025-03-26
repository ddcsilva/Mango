using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MicroStore.Services.ProductAPI.Domain.Models;
using System.Reflection.Emit;

namespace MicroStore.Services.ProductAPI.Infrastructure.Mappings;

public class ProductMap : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.ProductId);

        builder.Property(p => p.Name)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(p => p.Price)
               .HasColumnType("decimal(18,2)");

        builder.Property(p => p.Description)
               .IsRequired()
               .HasMaxLength(1000);

        builder.Property(p => p.CategoryName)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(p => p.ImageUrl)
               .IsRequired()
        .HasMaxLength(200);

        builder.HasData(new Product
        {
            ProductId = 1,
            Name = "Samosa",
            Price = 15,
            Description = " Quisque vel lacus ac magna, vehicula sagittis ut non lacus.<br/> Vestibulum arcu turpis, maximus malesuada neque. Phasellus commodo cursus pretium.",
            ImageUrl = "https://placehold.co/603x403",
            CategoryName = "Appetizer"
        });

        builder.HasData(new Product
        {
            ProductId = 2,
            Name = "Paneer Tikka",
            Price = 13.99,
            Description = " Quisque vel lacus ac magna, vehicula sagittis ut non lacus.<br/> Vestibulum arcu turpis, maximus malesuada neque. Phasellus commodo cursus pretium.",
            ImageUrl = "https://placehold.co/602x402",
            CategoryName = "Appetizer"
        });

        builder.HasData(new Product
        {
            ProductId = 3,
            Name = "Sweet Pie",
            Price = 10.99,
            Description = " Quisque vel lacus ac magna, vehicula sagittis ut non lacus.<br/> Vestibulum arcu turpis, maximus malesuada neque. Phasellus commodo cursus pretium.",
            ImageUrl = "https://placehold.co/601x401",
            CategoryName = "Dessert"
        });

        builder.HasData(new Product
        {
            ProductId = 4,
            Name = "Pav Bhaji",
            Price = 15,
            Description = " Quisque vel lacus ac magna, vehicula sagittis ut non lacus.<br/> Vestibulum arcu turpis, maximus malesuada neque. Phasellus commodo cursus pretium.",
            ImageUrl = "https://placehold.co/600x400",
            CategoryName = "Entree"
        });
    }
}
