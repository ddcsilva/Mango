using Mango.Services.CouponAPI.Application.Interfaces;
using Mango.Services.CouponAPI.Domain.Models;
using Mango.Services.CouponAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.CouponAPI.Infrastructure.Repositories;

public class CouponRepository(AppDbContext context) : ICouponRepository
{
    public async Task<IEnumerable<Coupon>> GetAllAsync()
    {
        return await context.Coupons.ToListAsync();
    }

    public async Task<Coupon?> GetByIdAsync(int id)
    {
        return await context.Coupons.FindAsync(id);
    }

    public async Task<Coupon?> GetByCodeAsync(string code)
    {
        if (string.IsNullOrWhiteSpace(code)) return null;

        return await context.Coupons.FirstOrDefaultAsync(c => c.CouponCode.Equals(code, StringComparison.CurrentCultureIgnoreCase));
    }

    public async Task AddAsync(Coupon coupon)
    {
        await context.Coupons.AddAsync(coupon);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Coupon coupon)
    {
        context.Coupons.Update(coupon);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Coupon coupon)
    {
        context.Coupons.Remove(coupon);
        await context.SaveChangesAsync();
    }
}