using BankTransferApp.Domain.Handlers;

namespace BankTransferApp.Application.Handlers.Auth.UserSignIn;

public sealed class UserSignInHandler : IHandler<UserSignInCommand, CustomResultData<Guid>>
{
    public Task<CustomResultData<Guid>> HandleAsync(UserSignInCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
