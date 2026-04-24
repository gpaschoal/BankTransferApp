using BankTransferApp.Application.Shared;
using BankTransferApp.Domain.Handlers;
using BankTransferApp.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace BankTransferApp.Application.Handlers.Auth.UserSignIn;

public sealed class UserSignInHandler(
    ILogger<UserSignInHandler> logger,
    IUnitOfWork unitOfWork,
    IUserRepository userRepository) : IHandler<UserSignInCommand, ResultData<Guid>>
{
    public async Task<ResultData<Guid>> HandleAsync(UserSignInCommand request, CancellationToken cancellationToken)
    {
        UserSignInValidator validator = new();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return validationResult.FromValidator<ResultData<Guid>>();
        }

        ResultData<Guid> customResultData = new();

        try
        {
            await unitOfWork.BeginTransactionAsync();

            /* cool stuff happens here! */

            await unitOfWork.CommitTransactionAsync();

            return customResultData;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while processing UserSignInCommand.");
            await unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
}
