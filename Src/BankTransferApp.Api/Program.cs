using BankTransferApp.Api.Filters;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers(x =>
{
    x.Filters.Add<TransactionalFilter>();
});
builder.Services.AddOpenApi();

var app = builder.Build();

app.MapDefaultEndpoints();

app.MapOpenApi();
app.MapScalarApiReference(options =>
{
    options.Theme = ScalarTheme.Moon;
});
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
