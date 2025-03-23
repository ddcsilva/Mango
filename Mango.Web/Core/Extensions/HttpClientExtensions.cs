using Mango.Web.Core.Base;
using Mango.Web.Core.Utilities;
using Mango.Web.Features.Coupons.Interfaces;
using Mango.Web.Features.Coupons.Services;

namespace Mango.Web.Core.Extensions;

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
