using BankTransferApp.Domain.Entities;
using BankTransferApp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BankTransferApp.Infrastructure.Repositories;

public class BalancePerMonthRepository(AppDbContext dbContext) 
    : RepositoryBase<BalancePerMonthEntity>(dbContext), IBalancePerMonthRepository
{
    public async Task<BalancePerMonthEntity> GetBalanceAsync(Guid accountId, int currentMonthReference, int currentYearReference, CancellationToken cancellationToken)
    {
        return await Queryable
            .FirstOrDefaultAsync(b => b.AccountId == accountId 
                                        && b.Month == currentMonthReference 
                                        && b.Year == currentYearReference, cancellationToken);
    }
}