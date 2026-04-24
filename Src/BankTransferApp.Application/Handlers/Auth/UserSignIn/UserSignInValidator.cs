using FluentValidation;

namespace BankTransferApp.Application.Handlers.Auth.UserSignIn;

internal sealed class UserSignInValidator : AbstractValidator<UserSignInCommand>
{
    public UserSignInValidator()
    {

    }
}