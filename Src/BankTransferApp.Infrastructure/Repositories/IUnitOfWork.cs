using BankTransferApp.Domain.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace BankTransferApp.Infrastructure.Repositories;

public sealed class UnitOfWork(AppDbContext appDbContext) : IUnitOfWork
{
    private IDbContextTransaction? transaction;

    public async Task BeginTransactionAsync()
    {
        transaction = await appDbContext.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        if (transaction is null) return;

        await appDbContext.SaveChangesAsync();
        await transaction.CommitAsync();
        await transaction.DisposeAsync();
    }

    public async Task RollbackTransactionAsync()
    {
        if (transaction is null) return;

        await transaction.RollbackAsync();
        await transaction.DisposeAsync();
    }
}
