using Microsoft.EntityFrameworkCore;
using MicroStore.Services.CouponAPI.Domain.Models;
using MicroStore.Services.CouponAPI.Infrastructure.Mappings;

namespace MicroStore.Services.CouponAPI.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Coupon> Coupons { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new CouponMap());
    }
}
