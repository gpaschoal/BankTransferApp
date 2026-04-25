using BankTransferApp.Domain.Entities;
using BankTransferApp.Domain.Handlers;
using BankTransferApp.Domain.ValueObjects;

namespace BankTransferApp.Application.Handlers.Auth.UserSignIn;

public record UserSignInCommand(
        PersonNameValueObject Name,
        string Cpf,
        AddressValueObject Address,
        TelephoneValueObject Cellphone,
        TelephoneValueObject HomePhone,
        string Password,
        string PasswordConfirmation) : ICommand
{
    public UserEntity ToUserEntity(string hashedPassword) =>
        UserEntity.Create(
            name: Name,
            cpfDocument: new(Cpf),
            address: Address,
            cellphone: Cellphone,
            homePhone: HomePhone,
            password: new(hashedPassword)
        );
}
