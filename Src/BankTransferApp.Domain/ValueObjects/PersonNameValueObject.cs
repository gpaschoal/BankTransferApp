namespace BankTransferApp.Domain.ValueObjects;

public record PersonNameValueObject(string FirstName, string LastName)
{
    public string FullName => $"{FirstName} {LastName}";
}