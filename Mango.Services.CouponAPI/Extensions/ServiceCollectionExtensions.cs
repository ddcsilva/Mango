using Mango.Services.CouponAPI.Application.Interfaces;
using Mango.Services.CouponAPI.Application.Services;
using Mango.Services.CouponAPI.Infrastructure.Repositories;

namespace Mango.Services.CouponAPI.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDependencies(this IServiceCollection services)
    {
        services.AddScoped<ICouponService, CouponService>();
        services.AddScoped<ICouponRepository, CouponRepository>();
        return services;
    }
}
