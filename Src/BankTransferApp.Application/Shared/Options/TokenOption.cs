namespace BankTransferApp.Application.Shared.Options;

public sealed class TokenOption
{
    public string SecretKey { get; set; }
    public int ExpirationInHours { get; set; }
    public string Issuer { get; set; }
}
