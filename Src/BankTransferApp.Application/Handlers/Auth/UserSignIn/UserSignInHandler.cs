using BankTransferApp.Application.Shared;
using BankTransferApp.Application.Shared.Options;
using BankTransferApp.Domain.Handlers;
using BankTransferApp.Domain.Repositories;
using BankTransferApp.Domain.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BankTransferApp.Application.Handlers.Auth.UserSignIn;

public class UserSignInHandler(
    ILogger<UserSignInHandler> logger,
    IUserRepository userRepository,
    IPasswordHasher passwordHasher,
    ITokenService tokenService,
    IOptions<TokenOption> options) : IHandler<UserSignInCommand, Result<UserSignInResponse>>
{
    public async Task<Result<UserSignInResponse>> HandleAsync(
        UserSignInCommand request,
        CancellationToken cancellationToken)
    {
        UserSignInValidator validator = new();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid) return validationResult.FromValidator<Result<UserSignInResponse>>();

        try
        {
            var tokenOptions = options.Value;

            var user = await userRepository.GetUserByCpfAsync(request.Cpf, cancellationToken);

            if (user is null) return new("Authentication", "Invalid CPF or password.");

            var isPasswordValid = passwordHasher.Verify(request.Password, user.Password.Value);

            if (!isPasswordValid) return new("Authentication", "Invalid CPF or password.");

            var token = tokenService.GenerateToken(user, new Dictionary<string, string>());

            UserSignInResponse signInResponse = new()
            {
                Token = token,
                ExpireAt = DateTime.UtcNow.AddHours(tokenOptions.ExpirationInHours)
            };

            return new(signInResponse);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while processing UserSignInCommand.");
            throw;
        }
    }
}
