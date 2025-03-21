using FluentValidation;
using FluentValidation.AspNetCore;
using Mango.Web.Core.Base;
using Mango.Web.Core.Utilities;
using Mango.Web.Features.Coupons.Interfaces;
using Mango.Web.Features.Coupons.Services;
using Mango.Web.Features.Coupons.Validators;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews(options =>
{
    options.ModelValidatorProviders.Clear();
});

builder.Services.AddValidatorsFromAssemblyContaining<CouponDTOValidator>();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();

builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();
builder.Services.Configure<ServiceUrls>(builder.Configuration.GetSection("ServiceURLs"));

builder.Services.AddScoped<IBaseService, BaseService>();
builder.Services.AddScoped<ICouponService, CouponService>();

var app = builder.Build();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
