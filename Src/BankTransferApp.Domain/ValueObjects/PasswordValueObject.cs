namespace BankTransferApp.Domain.ValueObjects;

public class PasswordValueObject : IEquatable<string>
{
    public string Value { get; set; }

    public PasswordValueObject() { }

    public PasswordValueObject(string value)
    {
        Value = value;
    }

    public bool Equals(string? hash)
    {
        if (string.IsNullOrWhiteSpace(hash)) return false;

        return hash.Equals(Value);
    }
}