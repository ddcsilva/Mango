﻿using Mango.Services.CouponAPI.Domain.Models;

namespace Mango.Services.CouponAPI.Application.Interfaces;

public interface ICouponRepository
{
    Task<IEnumerable<Coupon>> GetAllAsync();
    Task<Coupon?> GetByIdAsync(int id);
    Task<Coupon?> GetByCodeAsync(string code);
    Task AddAsync(Coupon coupon);
    Task UpdateAsync(Coupon coupon);
    Task DeleteAsync(Coupon coupon);
}
