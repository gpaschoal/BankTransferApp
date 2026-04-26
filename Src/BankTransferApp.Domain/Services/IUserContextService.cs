namespace BankTransferApp.Domain.Services;

public interface IUserContextService
{
    public bool IsLoggedIn => CurrentUserId.HasValue && !CurrentUserId.Value.Equals(Guid.Empty);
    public Guid? CurrentUserId { get; }
    void SetCurrentUserId(Guid userId);

    public void ThrownsIfUserNotLoggedIn()
    {
        if (!IsLoggedIn) 
            throw new InvalidOperationException("User is not logged in.");
    }
}
