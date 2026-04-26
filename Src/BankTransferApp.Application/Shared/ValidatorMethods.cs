using BankTransferApp.Domain.Handlers;
using FluentValidation.Results;

namespace BankTransferApp.Application.Shared;

public static class ValidatorMethods
{
    public static Result ToResult(this ValidationResult validationResult)
    {
        if (validationResult.IsValid)
            throw new InvalidOperationException("At this point, the validation result should be invalid.");

        Result customResultData = new();

        foreach (var error in validationResult.Errors)
            customResultData.AddError(error.PropertyName, error.ErrorMessage);

        return customResultData;
    }

    public static Result<T> ToResult<T>(this ValidationResult validationResult)
        where T : new()
    {
        if (validationResult.IsValid)
            throw new InvalidOperationException("At this point, the validation result should be invalid.");

        Result<T> customResultData = new();

        foreach (var error in validationResult.Errors)
            customResultData.AddError(error.PropertyName, error.ErrorMessage);

        return customResultData;
    }
}
