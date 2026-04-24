using BankTransferApp.Domain.Handlers;
using BankTransferApp.Domain.ValueObjects;

namespace BankTransferApp.Application.Handlers.Auth.UserSignIn;

public record UserSignInCommand(
        PersonNameValueObject Name,
        CpfValueObject CpfDocument,
        AddressValueObject Address,
        TelephoneValueObject Cellphone,
        TelephoneValueObject HomePhone,
        PasswordValueObject Password) : ICommand;
