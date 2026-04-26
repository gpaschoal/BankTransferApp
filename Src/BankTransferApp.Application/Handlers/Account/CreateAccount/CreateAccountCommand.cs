using BankTransferApp.Domain.Enums;

namespace BankTransferApp.Application.Handlers.Account.CreateAccount;

public record CreateAccountCommand(EAccountType AccountType);
