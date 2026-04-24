using BankTransferApp.Domain.ValueObjects;
using FluentValidation;

namespace BankTransferApp.Application.Shared;

internal sealed class PersonNameValueObjectValidator : AbstractValidator<PersonNameValueObject>
{
    public PersonNameValueObjectValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .MaximumLength(100).WithMessage("First name must not exceed 100 characters.");

        RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(100).WithMessage("Last name must not exceed 100 characters.");
    }
}
