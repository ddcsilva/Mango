using Mango.Web.Core.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvcWithValidation();
builder.Services.AddAppFluentValidation();  
builder.Services.AddAppHttpClients(builder.Configuration);
builder.Services.AddDependencies();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
