using BankTransferApp.Domain.Services;

namespace BankTransferApp.Application.Service;

public class UserContextService : IUserContextService
{
    public Guid? CurrentUserId { get; private set; }

    public void SetCurrentUserId(Guid userId) => CurrentUserId = userId;
}
