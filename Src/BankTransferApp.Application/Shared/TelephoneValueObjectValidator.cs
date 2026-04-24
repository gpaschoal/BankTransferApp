using BankTransferApp.Domain.ValueObjects;
using FluentValidation;

namespace BankTransferApp.Application.Shared;

internal sealed class TelephoneValueObjectValidator : AbstractValidator<TelephoneValueObject>
{
    public TelephoneValueObjectValidator()
    {
        RuleFor(x => x.AreaCode)
            .NotEmpty().WithMessage("Area code is required.")
            .MaximumLength(3).WithMessage("Area code must not exceed 3 characters.");
        RuleFor(x => x.Number)
            .NotEmpty().WithMessage("Phone number is required.")
            .MaximumLength(10).WithMessage("Phone number must not exceed 10 characters.");
    }
}