using BankTransferApp.Application.Constants;
using BankTransferApp.Application.Shared.Options;
using BankTransferApp.Domain.Entities;
using BankTransferApp.Domain.Services;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BankTransferApp.Application.Service;

public sealed class TokenService(IOptions<TokenOption> options) : ITokenService
{
    public string GenerateToken(UserEntity user, IDictionary<string, string> claims)
    {
        var tokenOptions = options.Value;
        var key = Encoding.ASCII.GetBytes(tokenOptions.SecretKey);

        var claimsList = new List<Claim>() {
            new(ClaimTypes.Name, user.Name.FullName),
            new(JwtCustomClaims.USER_IDENTIFIER, user.Id.ToString())
        };

        foreach (var claim in claims)
        {
            claimsList.Add(new Claim(claim.Key, claim.Value));
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claimsList),
            Expires = DateTime.UtcNow.AddHours(tokenOptions.ExpirationInHours),
            Issuer = tokenOptions.Issuer,
            SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
