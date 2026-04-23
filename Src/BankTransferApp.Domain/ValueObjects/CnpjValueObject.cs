namespace BankTransferApp.Domain.ValueObjects;

public class CnpjValueObject : AbstractDocumentValueObject
{
    public CnpjValueObject(string value)
    {
        Value = Normalize(value);

        if (!IsValid(Value))
            throw new ArgumentException("Invalid CNPJ");
    }

    public override string Normalize(string value)
    {
        throw new NotImplementedException();
    }

    protected override bool IsValid(string value)
    {
        // Implement CNPJ validation logic here
        return string.IsNullOrWhiteSpace(value);
    }
}