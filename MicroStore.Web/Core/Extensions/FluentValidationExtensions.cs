using FluentValidation.AspNetCore;
using FluentValidation;
using Mango.Web.Features.Auth.Validators;

namespace Mango.Web.Core.Extensions;

public static class FluentValidationExtensions
{
    public static IServiceCollection AddAppFluentValidation(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<LoginValidator>();
        services.AddFluentValidationAutoValidation();
        services.AddFluentValidationClientsideAdapters();
        return services;
    }
}
