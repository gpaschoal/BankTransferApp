namespace BankTransferApp.Domain.Entities;

public class WithdrawalEntity : IEntity
{
    public Guid Id { get; set; }
    public decimal Value { get; set; }
    public DateTime Reference { get; set; }
    public Guid AccountId { get; set; }
    public AccountEntity Account { get; set; }
    public Guid TransactionId { get; set; }
    public TransactionEntity Transaction { get; set; }
}
