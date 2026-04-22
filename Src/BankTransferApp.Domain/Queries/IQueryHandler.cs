namespace BankTransferApp.Domain.Queries;

public interface IQueryHandler<in TRequest, TResult>
    where TRequest : QueryCommand
    where TResult : class, new()
{
    Task<PageResponse<TResult>> ExecuteAsync(TRequest request, CancellationToken cancellationToken);
}
