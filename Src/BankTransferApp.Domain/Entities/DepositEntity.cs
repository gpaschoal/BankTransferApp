namespace BankTransferApp.Domain.Entities;

public class DepositEntity : IEntity
{
    public Guid Id { get; set; }
    public decimal Value { get; set; }
    public DateTime Reference { get; set; }
    public Guid AccountId { get; set; }
    public AccountEntity Account { get; set; }
    public Guid TransactionId { get; set; }
    public TransactionEntity Transaction { get; set; }

    public static DepositEntity Create(
        decimal value,
        Guid accountId,
        Guid transactionId)
    {
        return new DepositEntity
        {
            Id = Guid.CreateVersion7(),
            Value = value,
            Reference = DateTime.UtcNow,
            AccountId = accountId,
            TransactionId = transactionId
        };
    }
}
