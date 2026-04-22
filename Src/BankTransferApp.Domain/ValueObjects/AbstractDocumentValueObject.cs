namespace BankTransferApp.Domain.ValueObjects;

public abstract class AbstractDocumentValueObject
{
    public string Value { get; protected set; }

    public abstract string Normalize(string value);
}
