namespace BankTransferApp.Domain.ValueObjects;

public class PasswordValueObject
{
    public string Value { get; set; }

    public PasswordValueObject() { }

    public PasswordValueObject(string value)
    {
        Value = value;
    }
}