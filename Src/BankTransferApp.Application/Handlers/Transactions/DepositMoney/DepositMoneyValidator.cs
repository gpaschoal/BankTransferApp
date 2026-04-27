using FluentValidation;

namespace BankTransferApp.Application.Handlers.Transactions.DepositMoney;

public class DepositMoneyValidator : AbstractValidator<DepositMoneyCommand>
{
    public DepositMoneyValidator()
    {
        RuleFor(x => x.AccountId).NotEmpty().WithMessage("AccountId is required.");
        RuleFor(x => x.Amount).GreaterThan(0).WithMessage("Amount must be greater than zero.")
            .LessThanOrEqualTo(10000).WithMessage("Amount must be less than or equal to 10000.");
    }
}
