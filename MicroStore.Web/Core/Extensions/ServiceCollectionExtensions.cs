using MicroStore.Web.Core.Base;
using MicroStore.Web.Features.Auth.Interfaces;
using MicroStore.Web.Features.Auth.Services;
using MicroStore.Web.Features.Coupons.Interfaces;
using MicroStore.Web.Features.Coupons.Services;

namespace MicroStore.Web.Core.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDependencies(this IServiceCollection services)
    {
        services.AddScoped<IBaseService, BaseService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<ICouponService, CouponService>();

        return services;
    }
}
