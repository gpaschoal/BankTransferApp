using FluentValidation;

namespace BankTransferApp.Application.Handlers.Account.ActivateAccount;

public class ActivateAccountCommandValidator : AbstractValidator<ActivateAccountCommand>
{
    public ActivateAccountCommandValidator()
    {
        RuleFor(x => x.AccountId)
            .NotEmpty().WithMessage("Account ID is required.")
            .Must(id => id != Guid.Empty).WithMessage("Account ID must be a valid GUID.");
    }
}