namespace BankTransferApp.Domain.Services;

public interface IPasswordHasher
{
    string Hash(string password);
}
