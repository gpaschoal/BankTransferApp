using BankTransferApp.Application.Shared.Commands;
using FluentValidation;

namespace BankTransferApp.Application.Handlers.Auth.UserSignUp;

<<<<<<<< HEAD:Src/BankTransferApp.Application/Handlers/Auth/UserSignUp/UserSignUpValidator.cs
internal sealed class UserSignUpValidator : AbstractValidator<UserSignUpCommand>
========
internal sealed class UserSignInValidator : AbstractValidator<UserSignUpCommand>
>>>>>>>> 2f95de9a5784a33b0cb5be51d01cf135a0aab462:Src/BankTransferApp.Application/Handlers/Auth/UserSignUp/UserSignInValidator.cs
{
    public UserSignUpValidator()
    {
        RuleFor(x => x.Name).SetValidator(new PersonNameCommandValidator());

        RuleFor(x => x.Cpf)
            .NotEmpty().WithMessage("CPF is required.")
            .Length(11).WithMessage("CPF must be exactly 11 characters.");

        RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");

        RuleFor(x => x.PasswordConfirmation).NotEmpty().WithMessage("Password confirmation is required.")
            .Equal(x => x.Password).WithMessage("Password confirmation does not match the password.");

        RuleFor(x => x.Address).NotEmpty().WithMessage("Address is required.")
            .SetValidator(new AddressCommandValidator());

        RuleFor(x => x.Cellphone).NotEmpty().WithMessage("Cellphone is required.")
            .SetValidator(new TelephoneCommandValidator());

        RuleFor(x => x.HomePhone).NotEmpty().WithMessage("Home phone is required.")
            .SetValidator(new TelephoneCommandValidator());
    }
}