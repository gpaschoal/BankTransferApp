namespace BankTransferApp.Domain.Entities;

public class BalancePerMonthEntity : IEntity
{
    public Guid Id { get; set; }
    public decimal Balance { get; set; }
    public int Year { get; set; }
    public int Month { get; set; }
    public Guid AccountId { get; set; }
    public AccountEntity Account { get; set; }
    public ICollection<TransactionEntity> Transactions { get; set; } = [];

    public static BalancePerMonthEntity Create(
        Guid accountId,
        int monthReference,
        int yearReference)
    {
        return new BalancePerMonthEntity
        {
            Id = Guid.CreateVersion7(),
            AccountId = accountId,
            Month = monthReference,
            Year = yearReference,
            Balance = 0
        };
    }
}
