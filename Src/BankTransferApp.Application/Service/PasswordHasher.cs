using BankTransferApp.Domain.Services;

namespace BankTransferApp.Application.Service;

public class PasswordHasher : IPasswordHasher
{
    public string Hash(string password) => BCrypt.Net.BCrypt.HashPassword(password);
}
