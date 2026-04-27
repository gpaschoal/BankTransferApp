using BankTransferApp.Domain.Entities;
using BankTransferApp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BankTransferApp.Infrastructure.Repositories;

public abstract class RepositoryBase<T>(AppDbContext dbContext) : IRepository<T>
    where T : class, IEntity
{
    protected DbSet<T> Queryable => dbContext.Set<T>();

    public async Task AddAsync(T entity, CancellationToken cancellationToken)
    {
        await Queryable.AddAsync(entity, cancellationToken);
    }

    public Task DeleteAsync(T entity, CancellationToken cancellationToken)
    {
        if (entity is IAuditedFields auditedFields)
        {
            auditedFields.DeletedAt ??= DateTime.UtcNow;
            dbContext.Entry(entity).State = EntityState.Modified;
        }
        else Queryable.Remove(entity);

        return Task.CompletedTask;
    }

    public Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken)
    {
        return Queryable.AnyAsync(e => e.Id == id, cancellationToken);
    }

    public async Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await Queryable.FindAsync([id], cancellationToken);
    }

    public Task UpdateAsync(T entity, CancellationToken cancellationToken)
    {
        dbContext.Entry(entity).State = EntityState.Modified;
        return Task.CompletedTask;
    }
}
