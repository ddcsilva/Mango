using Microsoft.AspNetCore.Authentication.Cookies;
using MicroStore.Web.Core.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvcWithValidation();
builder.Services.AddAppFluentValidation();
builder.Services.AddAppHttpClients(builder.Configuration);
builder.Services.AddDependencies();
builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromHours(10);
        options.LoginPath = "/Auth/Login";
        options.AccessDeniedPath = "/Auth/AccessDenied";
    });


var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
