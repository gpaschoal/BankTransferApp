using BankTransferApp.Application.Handlers.Account.ActivateAccount;
using BankTransferApp.Application.Handlers.Account.CreateAccount;
using BankTransferApp.Application.Handlers.Account.DeactivateAccount;
using BankTransferApp.Application.Handlers.Auth.UserSignIn;
using BankTransferApp.Application.Handlers.Auth.UserSignUp;
using BankTransferApp.Application.Handlers.Transactions.DepositMoney;
using BankTransferApp.Application.Service;
using BankTransferApp.Domain.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BankTransferApp.Application;

public static class IoCApplication
{
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        // Authorization
        services.AddScoped<UserSignUpHandler>();
        services.AddScoped<UserSignInHandler>();

        // Account
        services.AddScoped<CreateAccountHandler>();
        services.AddScoped<DeactivateAccountHandler>();
        services.AddScoped<ActivateAccountHandler>();
        services.AddScoped<DepositMoneyHandler>();

        services.AddScoped<IPasswordHasherService, PasswordHasherService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IUserContextService, UserContextService>();
    }
}