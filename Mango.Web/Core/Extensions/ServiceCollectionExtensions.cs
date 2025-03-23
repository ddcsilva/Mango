using Mango.Web.Core.Base;
using Mango.Web.Features.Coupons.Interfaces;
using Mango.Web.Features.Coupons.Services;

namespace Mango.Web.Core.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDependencies(this IServiceCollection services)
    {
        services.AddScoped<IBaseService, BaseService>();
        services.AddScoped<ICouponService, CouponService>();

        return services;
    }
}
