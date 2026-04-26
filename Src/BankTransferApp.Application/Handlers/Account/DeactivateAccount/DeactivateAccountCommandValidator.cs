using FluentValidation;

namespace BankTransferApp.Application.Handlers.Account.DeactivateAccount;

public class DeactivateAccountCommandValidator : AbstractValidator<DeactivateAccountCommand>
{
    public DeactivateAccountCommandValidator()
    {
        RuleFor(x => x.AccountId)
            .NotEmpty().WithMessage("AccountId is required.")
            .Must(id => id != Guid.Empty).WithMessage("AccountId must be a valid GUID.");
    }
}
