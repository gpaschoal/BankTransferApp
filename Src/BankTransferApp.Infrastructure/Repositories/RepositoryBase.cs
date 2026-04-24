using BankTransferApp.Domain.Entities;
using BankTransferApp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BankTransferApp.Infrastructure.Repositories;

public abstract class RepositoryBase<T>(AppDbContext dbContext) : IRepository<T>
    where T : class, IEntity
{
    protected DbSet<T> Queryable => dbContext.Set<T>();

    public async Task CreateAsync(T entity, CancellationToken cancellationToken)
    {
        await Queryable.AddAsync(entity, cancellationToken);
    }

    public async Task DeleteAsync(T entity, CancellationToken cancellationToken)
    {
        await Queryable.AddAsync(entity, cancellationToken);
    }

    public async Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await Queryable.FindAsync([id], cancellationToken);
    }

    public async Task UpdateAync(T entity, CancellationToken cancellationToken)
    {
        await Queryable.AddAsync(entity, cancellationToken);
    }
}
