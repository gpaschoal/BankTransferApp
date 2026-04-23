using BankTransferApp.Api.Attributes;
using BankTransferApp.Domain.Repositories;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BankTransferApp.Api.Filters;

public class TransactionalFilter(IUnitOfWork unitOfWork) : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(
        ActionExecutingContext context,
        ActionExecutionDelegate next)
    {
        var hasAttribute = context.ActionDescriptor.EndpointMetadata
            .Any(m => m is TransactionalAttribute);

        if (!hasAttribute)
        {
            await next();
            return;
        }

        await unitOfWork.BeginTransactionAsync();

        try
        {
            var result = await next();

            if (result.Exception == null)
            {
                await unitOfWork.CommitTransactionAsync();
                return;
            }

            await unitOfWork.RollbackTransactionAsync();
        }
        catch
        {
            await unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
}