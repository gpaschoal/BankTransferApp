namespace BankTransferApp.Application.Handlers.Auth.UserSignIn;

public record UserSignInCommand(string Cpf, string Password);
