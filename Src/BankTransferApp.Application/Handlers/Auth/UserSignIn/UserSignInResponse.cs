namespace BankTransferApp.Application.Handlers.Auth.UserSignIn;

public record UserSignInResponse
{
    public string Token { get; init; }
    public DateTime ExpireAt { get; init; }
}