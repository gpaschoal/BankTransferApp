using BankTransferApp.Domain.Entities;

namespace BankTransferApp.Domain.Repositories;

public interface IRepository<T>
    where T : class, IEntity
{
    Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task CreateAsync(T entity, CancellationToken cancellationToken);

    Task UpdateAync(T entity, CancellationToken cancellationToken);

    Task DeleteAsync(T entity, CancellationToken cancellationToken);
}
