using FluentValidation;

namespace BankTransferApp.Application.Handlers.Auth.UserSignIn;

public class UserSignInValidator : AbstractValidator<UserSignInCommand>
{
    public UserSignInValidator()
    {
        RuleFor(x => x.Cpf).NotEmpty().WithMessage("CPF is required.");
        RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required.");
    }
}
