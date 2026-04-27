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

    public static TransferEntity Create(
        decimal value,
        Guid sourceAccountId,
        Guid destinationAccountId)
    {
        if (value <= 0) throw new ArgumentException("Value must be greater than 0.");
        return new TransferEntity
        {
            Id = Guid.CreateVersion7(),
            Value = value,
            Reference = DateTime.UtcNow,
            SourceAccountId = sourceAccountId,
            DestinationAccountId = destinationAccountId
        };
    }
}
