using BankTransferApp.Domain.Entities;
using BankTransferApp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BankTransferApp.Infrastructure.Repositories;

public class BalancePerMonthEntityRepository(AppDbContext dbContext) 
    : RepositoryBase<BalancePerMonthEntity>(dbContext), IBalancePerMonthEntityRepository
{
    public async Task<BalancePerMonthEntity> GetBalanceAsync(Guid accountId, int currentMonthReference, int currentYearReference, CancellationToken cancellationToken)
    {
        return await Queryable
            .FirstOrDefaultAsync(b => b.AccountId == accountId 
                                        && b.Month == currentMonthReference 
                                        && b.Year == currentYearReference, cancellationToken);
    }
}