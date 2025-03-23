using Mango.Services.AuthAPI.Application.Interfaces;
using Mango.Services.AuthAPI.Application.Services;

namespace Mango.Services.AuthAPI.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDependencies(this IServiceCollection services)
    {
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddScoped<IAuthService, AuthService>();

        return services;
    }
}
