using BankTransferApp.Application.Shared;
using BankTransferApp.Domain.Handlers;
using BankTransferApp.Domain.Repositories;
using BankTransferApp.Domain.Services;
using Microsoft.Extensions.Logging;

namespace BankTransferApp.Application.Handlers.Auth.UserSignUp;

public sealed class UserSignUpHandler(
    ILogger<UserSignUpHandler> logger,
    IUnitOfWork unitOfWork,
    IUserRepository userRepository,
    IPasswordHasher passwordHasher) : IHandler<UserSignUpCommand, Result<Guid>>
{
    public async Task<Result<Guid>> HandleAsync(UserSignUpCommand request, CancellationToken cancellationToken)
    {
        UserSignUpValidator validator = new();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return validationResult.FromValidator<Result<Guid>>();
        }

        Result<Guid> customResultData = new();

        try
        {
            var existingUser = await userRepository.UserExistsByCpfAsync(request.Cpf, cancellationToken);

            if (existingUser)
            {
                customResultData.AddError(nameof(request.Cpf), "A user with the provided CPF already exists.");
                return customResultData;
            }

            await unitOfWork.BeginTransactionAsync(cancellationToken);

            var hashedPassword = passwordHasher.Hash(request.Password);

            var user = request.ToEntity(hashedPassword);

            await userRepository.CreateAsync(user, cancellationToken);

            await unitOfWork.CommitTransactionAsync(cancellationToken);

            return new(user.Id);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while processing UserSignInCommand.");
            await unitOfWork.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }
}
