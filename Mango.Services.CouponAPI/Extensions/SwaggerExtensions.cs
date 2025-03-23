using Microsoft.OpenApi.Models;

namespace Mango.Services.CouponAPI.Extensions;

public static class SwaggerExtensions
{
    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Mango Coupon API",
                Version = "v1"
            });
        });

        return services;
    }
}
