namespace Mango.Web.Core.Extensions;

public static class MvcExtensions
{
    public static IServiceCollection AddMvcWithValidation(this IServiceCollection services)
    {
        services.AddControllersWithViews(options =>
        {
            options.ModelValidatorProviders.Clear();
        });

        return services;
    }
}
