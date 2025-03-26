using MicroStore.Web.Core.Base;
using MicroStore.Web.Core.Utilities;
using MicroStore.Web.Features.Coupons.Interfaces;
using MicroStore.Web.Features.Coupons.Services;

namespace MicroStore.Web.Core.Extensions;

public static class HttpClientExtensions
{
    public static IServiceCollection AddAppHttpClients(this IServiceCollection services, IConfiguration config)
    {
        services.AddHttpContextAccessor();
        services.AddHttpClient();
        services.Configure<ServiceUrls>(config.GetSection("ServiceURLs"));

        services.AddScoped<IBaseService, BaseService>();
        services.AddScoped<ICouponService, CouponService>();

        return services;
    }
}
