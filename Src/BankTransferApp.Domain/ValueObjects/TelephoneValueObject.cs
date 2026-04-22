namespace BankTransferApp.Domain.ValueObjects;

public record TelephoneValueObject(string AreaCode, string Number);

public class PasswordValueObject(string Value)
{
    public bool IsEqual(string hash)
    {
        if (string.IsNullOrWhiteSpace(hash)) return false;

        return hash.Equals(Value);
    }
}