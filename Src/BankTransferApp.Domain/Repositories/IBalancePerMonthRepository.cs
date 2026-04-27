using BankTransferApp.Domain.Entities;

namespace BankTransferApp.Domain.Repositories;

public interface IBalancePerMonthRepository : IRepository<BalancePerMonthEntity>
{
    Task<BalancePerMonthEntity> GetBalanceAsync(Guid accountId, int currentMonthReference, int currentYearReference, CancellationToken cancellationToken);
}