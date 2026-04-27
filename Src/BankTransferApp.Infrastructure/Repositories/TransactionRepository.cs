using BankTransferApp.Domain.Entities;
using BankTransferApp.Domain.Repositories;

namespace BankTransferApp.Infrastructure.Repositories;

public class TransactionRepository(AppDbContext dbContext) : RepositoryBase<TransactionEntity>(dbContext), ITransactionRepository
{
}
