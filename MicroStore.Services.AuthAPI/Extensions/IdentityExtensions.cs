using Microsoft.AspNetCore.Identity;
using MicroStore.Services.AuthAPI.Domain.Models;
using MicroStore.Services.AuthAPI.Infrastructure.Data;

namespace MicroStore.Services.AuthAPI.Extensions;

public static class IdentityExtensions
{
    public static IServiceCollection AddIdentity(this IServiceCollection services)
    {
        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();
        return services;
    }
}
