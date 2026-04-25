using BankTransferApp.Domain.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace BankTransferApp.Infrastructure.Repositories;

public sealed class UnitOfWork(AppDbContext appDbContext) : IUnitOfWork
{
    private IDbContextTransaction transaction;

    public async Task BeginTransactionAsync(CancellationToken cancellationToken)
    {
        transaction = await appDbContext.Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task CommitTransactionAsync(CancellationToken cancellationToken)
    {
        if (transaction is null) return;

        await appDbContext.SaveChangesAsync(cancellationToken);
        await transaction.CommitAsync(cancellationToken);
        await transaction.DisposeAsync();
    }

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken)
    {
        if (transaction is null) return;

        await transaction.RollbackAsync(cancellationToken);
        await transaction.DisposeAsync();
    }
}
