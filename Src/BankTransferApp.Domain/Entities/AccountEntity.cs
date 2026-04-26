namespace BankTransferApp.Domain.Entities;

public class AccountEntity : IEntity, IAuditedFields, IActivableEntity
{
    public Guid Id { get; set; }
    public Guid OwnerId { get; set; }
    public UserEntity Owner { get; set; }
    public long AccountNumber { get; set; }
    public decimal Balance { get; set; }
    public bool IsActive { get; set; }
    public AccountType AccountType { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid CreatedById { get; set; }
    public UserEntity CreatedBy { get; set; }
    public DateTime? ModifiedAt { get; set; }
    public Guid? ModifiedById { get; set; }
    public UserEntity ModifiedBy { get; set; }
    public DateTime? DeletedAt { get; set; }
    public Guid? DeletedById { get; set; }
    public UserEntity DeletedBy { get; set; }
}

public enum AccountType
{
    /// <summary>
    ///  Conta corrente
    /// </summary>
    CurrentAccount,
    /// <summary>
    ///  Conta poupança
    /// </summary>
    SavingsAccount,
    /// <summary>
    ///  Conta salário
    /// </summary>
    PayrollAccount
}