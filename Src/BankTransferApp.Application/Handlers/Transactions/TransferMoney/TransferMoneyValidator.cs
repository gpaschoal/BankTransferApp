using FluentValidation;

namespace BankTransferApp.Application.Handlers.Transactions.TransferMoney;

public class TransferMoneyValidator : AbstractValidator<TransferMoneyCommand>
{
    public TransferMoneyValidator()
    {
        RuleFor(x => x.SourceAccountId).NotEmpty().WithMessage("Source account ID is required.");
        RuleFor(x => x.DestinationAccountId).NotEmpty().WithMessage("Destination account ID is required.")
            .NotEqual(x => x.SourceAccountId).WithMessage("Source account ID must be different from destination account ID");
        RuleFor(x => x.Amount).GreaterThan(0).WithMessage("Amount must be greater than zero.");
    }
}
