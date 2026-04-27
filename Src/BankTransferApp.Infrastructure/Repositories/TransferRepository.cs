using BankTransferApp.Domain.Entities;
using BankTransferApp.Domain.Repositories;

namespace BankTransferApp.Infrastructure.Repositories;

public class TransferRepository(AppDbContext dbContext) : RepositoryBase<TransferEntity>(dbContext), ITransferRepository
{
}
