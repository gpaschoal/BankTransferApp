using BankTransferApp.Api.Middlewares;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.AddOptions();
builder.AddJwtAuthentication();

var app = builder.Build();

app.MapDefaultEndpoints();

app.MapOpenApi();
app.MapScalarApiReference(options =>
{
    options.Theme = ScalarTheme.Moon;
});

app.UseMiddleware<UserContextMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapJwtAuthorization();

await app.RunAsync();
