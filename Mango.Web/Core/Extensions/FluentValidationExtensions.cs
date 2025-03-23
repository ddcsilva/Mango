using FluentValidation.AspNetCore;
using FluentValidation;
using Mango.Web.Features.Coupons.Validators;

namespace Mango.Web.Core.Extensions;

public static class FluentValidationExtensions
{
    public static IServiceCollection AddAppFluentValidation(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<CouponDTOValidator>();
        services.AddFluentValidationAutoValidation();
        services.AddFluentValidationClientsideAdapters();
        return services;
    }
}
