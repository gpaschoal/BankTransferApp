using BankTransferApp.Domain.Entities;

namespace BankTransferApp.Domain.Services;

public interface ITokenService
{
    string GenerateToken(UserEntity user, IDictionary<string, string> claims);
}
