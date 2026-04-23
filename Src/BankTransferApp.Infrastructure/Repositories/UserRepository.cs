using BankTransferApp.Domain.Entities;
using BankTransferApp.Domain.Repositories;

namespace BankTransferApp.Infrastructure.Repositories;

public class UserRepository(AppDbContext dbContext) : RepositoryBase<UserEntity>(dbContext), IUserRepository
{
}
