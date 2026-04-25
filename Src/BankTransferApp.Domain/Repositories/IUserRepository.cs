using BankTransferApp.Domain.Entities;

namespace BankTransferApp.Domain.Repositories;

public interface IUserRepository : IRepository<UserEntity>
{
    Task<UserEntity> GetUserByCpfAsync(string cpf, CancellationToken cancellationToken);

    public Task<bool> UserExistsByCpfAsync(string cpf, CancellationToken cancellationToken);
}
