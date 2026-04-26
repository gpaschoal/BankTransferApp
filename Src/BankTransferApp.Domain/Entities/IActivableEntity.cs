namespace BankTransferApp.Domain.Entities;

public interface IActivableEntity
{
    bool IsActive { get; set; }
    void InactiveAccount() => IsActive = false;
    void ActiveAccount() => IsActive = true;
}