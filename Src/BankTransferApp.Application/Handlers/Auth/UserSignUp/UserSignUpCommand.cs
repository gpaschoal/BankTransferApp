using BankTransferApp.Application.Shared.Commands;
using BankTransferApp.Domain.Entities;
using BankTransferApp.Domain.Handlers;

namespace BankTransferApp.Application.Handlers.Auth.UserSignUp;

public record UserSignUpCommand(
        PersonNameCommand Name,
        string Cpf,
        AddressCommand Address,
        TelephoneCommand Cellphone,
        TelephoneCommand HomePhone,
        string Password,
        string PasswordConfirmation) : ICommand
{
    public UserEntity ToEntity(string hashedPassword) =>
        UserEntity.Create(
            name: new(Name.FirstName, Name.LastName),
            cpfDocument: new(Cpf),
            address: new(Address.Street, Address.City, Address.State, Address.ZipCode),
            cellphone: new(Cellphone.AreaCode, Cellphone.Number),
            homePhone: new(HomePhone.AreaCode, HomePhone.Number),
            password: new(hashedPassword)
        );
}
