using BankTransferApp.Domain.Entities;
using BankTransferApp.Domain.Repositories;

namespace BankTransferApp.Infrastructure.Repositories;

public class AccountRepository(AppDbContext dbContext) : RepositoryBase<AccountEntity>(dbContext), IAccountRepository
{ 
}