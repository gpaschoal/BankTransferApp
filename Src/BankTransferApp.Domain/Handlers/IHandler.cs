namespace BankTransferApp.Domain.Handlers;

public interface IHandler<in TRequest, TResult>
    where TRequest : class, ICommand
    where TResult : Result, new()
{
    Task<TResult> HandleAsync(TRequest request, CancellationToken cancellationToken);
}
