namespace BankTransferApp.Domain.Handlers;

public interface ICommandHandler<in TRequest, TResult>
    where TRequest : class, ICommand
    where TResult : CustomResultData, new()
{
    Task<TResult> HandleAsync(TRequest request, CancellationToken cancellationToken);
}
