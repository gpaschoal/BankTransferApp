using BankTransferApp.Application.Handlers.Auth.UserSignIn;
using BankTransferApp.Application.Handlers.Auth.UserSignUp;
using BankTransferApp.Application.Service;
using BankTransferApp.Domain.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BankTransferApp.Application;

public static class IoCApplication
{
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<UserSignUpHandler>();
        services.AddScoped<UserSignInHandler>();

        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<ITokenService, TokenService>();
    }
}