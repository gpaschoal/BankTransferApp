using BankTransferApp.Domain.Entities;

namespace BankTransferApp.Domain.Repositories;

public interface IRepository<T>
    where T : class, IEntity
{
    Task<T> GetByIdAsync(Guid id);

    Task CreateAsync(T entity);

    Task UpdateAync(T entity);

    Task DeleteAsync(Guid id);
}
