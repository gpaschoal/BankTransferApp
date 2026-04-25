using BankTransferApp.Application.Shared.Commands;
using FluentValidation;

namespace BankTransferApp.Application.Handlers.Auth.UserSignUp;

internal sealed class UserSignInValidator : AbstractValidator<UserSignUpCommand>
{
    public UserSignInValidator()
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