using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MicroStore.Services.CouponAPI.Domain.Models;

namespace MicroStore.Services.CouponAPI.Infrastructure.Mappings;

public class CouponMap : IEntityTypeConfiguration<Coupon>
{
    public void Configure(EntityTypeBuilder<Coupon> builder)
    {
        builder.HasKey(c => c.CouponId);

        builder.Property(c => c.CouponCode)
               .IsRequired()
               .HasMaxLength(20);

        builder.Property(c => c.DiscountAmount)
               .HasColumnType("decimal(18,2)");

        builder.Property(c => c.MinAmount)
               .HasColumnType("decimal(18,2)");

        builder.HasIndex(c => c.CouponCode)
               .IsUnique();

        builder.HasData(
            new Coupon { CouponId = 1, CouponCode = "10OFF", DiscountAmount = 10, MinAmount = 50 },
            new Coupon { CouponId = 2, CouponCode = "20OFF", DiscountAmount = 20, MinAmount = 100 }
        );
    }
}
