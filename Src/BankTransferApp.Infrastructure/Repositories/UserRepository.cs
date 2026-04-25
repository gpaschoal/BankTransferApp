using BankTransferApp.Domain.Entities;
using BankTransferApp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BankTransferApp.Infrastructure.Repositories;

public class UserRepository(AppDbContext dbContext) : RepositoryBase<UserEntity>(dbContext), IUserRepository
{
    public async Task<bool> UserExistsByCpfAsync(string cpf, CancellationToken cancellationToken)
    {
        return await Queryable.AnyAsync(u => u.CpfDocument.Value == cpf, cancellationToken);
    }
}
