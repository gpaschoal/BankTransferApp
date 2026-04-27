using BankTransferApp.Domain.Entities;
using BankTransferApp.Domain.Repositories;

namespace BankTransferApp.Infrastructure.Repositories;

public class DepositRepository(AppDbContext dbContext) : RepositoryBase<DepositEntity>(dbContext), IDepositRepository
{
}
