namespace BankTransferApp.Domain.Entities;

public class TransferEntity : IEntity
{
    public Guid Id { get; set; }
    public decimal Value { get; set; }
    public DateTime Reference { get; set; }
    public Guid SourceAccountId { get; set; }
    public AccountEntity SourceAccount { get; set; }
    public Guid DestinationAccountId { get; set; }
    public AccountEntity DestinationAccount { get; set; }
}
