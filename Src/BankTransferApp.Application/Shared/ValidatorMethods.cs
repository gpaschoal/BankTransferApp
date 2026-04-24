using BankTransferApp.Domain.Handlers;
using FluentValidation.Results;

namespace BankTransferApp.Application.Shared;

public static class ValidatorMethods
{
    public static R FromValidator<R>(this ValidationResult validationResult)
        where R : ResultData, new()
    {
        if (validationResult.IsValid)
            throw new InvalidOperationException("At this point, the validation result should be invalid.");

        R customResultData = new();

        foreach (var error in validationResult.Errors)
            customResultData.AddError(error.PropertyName, error.ErrorMessage);

        return customResultData;
    }
}
