using MicroStore.Services.CouponAPI.Application.Interfaces;
using MicroStore.Services.CouponAPI.Application.Services;
using MicroStore.Services.CouponAPI.Infrastructure.Repositories;

namespace MicroStore.Services.CouponAPI.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDependencies(this IServiceCollection services)
    {
        services.AddScoped<ICouponService, CouponService>();
        services.AddScoped<ICouponRepository, CouponRepository>();
        return services;
    }
}
