using FluentValidation;

namespace BankTransferApp.Application.Handlers.Transactions.WithdrawMoney;

public class WithdrawMoneyValidator : AbstractValidator<WithdrawMoneyCommand>
{
    public WithdrawMoneyValidator()
    {
        RuleFor(x => x.AccountId).NotEmpty().WithMessage("AccountId is required.");
        RuleFor(x => x.Amount).LessThan(0).WithMessage("Value must be less than 0.");
    }
}
