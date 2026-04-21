namespace BankTransferApp.Domain.Queries;

public interface IQueryHandler<in TRequest, TResult>
{
    Task<TResult> Execute(TRequest request);
}
