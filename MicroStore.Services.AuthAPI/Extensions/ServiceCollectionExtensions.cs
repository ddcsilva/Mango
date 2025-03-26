using MicroStore.Services.AuthAPI.Application.Interfaces;
using MicroStore.Services.AuthAPI.Application.Services;

namespace MicroStore.Services.AuthAPI.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDependencies(this IServiceCollection services)
    {
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IAuthService, AuthService>();

        return services;
    }
}
