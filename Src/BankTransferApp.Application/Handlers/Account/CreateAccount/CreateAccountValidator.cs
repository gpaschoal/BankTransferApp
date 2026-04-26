using FluentValidation;

namespace BankTransferApp.Application.Handlers.Account.CreateAccount;

public class CreateAccountValidator : AbstractValidator<CreateAccountCommand>
{
    public CreateAccountValidator()
    {
            RuleFor(x => x.AccountType)
                .IsInEnum()
                .WithMessage("Invalid account type.");
    }
}
