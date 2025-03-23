using Mango.Services.AuthAPI.Domain.Models;
using Mango.Services.AuthAPI.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;

namespace Mango.Services.AuthAPI.Extensions;

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
