using Mango.Services.AuthAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddIdentity();
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddDependencies();
builder.Services.AddSwaggerWithJwt();
builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.ApplyMigrations();

app.Run();