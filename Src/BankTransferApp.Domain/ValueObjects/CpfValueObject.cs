namespace BankTransferApp.Domain.ValueObjects;

public class CpfValueObject : AbstractDocumentValueObject
{
    public CpfValueObject() { }

    public CpfValueObject(string value)
    {
        Value = Normalize(value);
        if (!IsValid(Value))
            throw new ArgumentException("Invalid CPF");
    }

    public override string Normalize(string value)
    {
        throw new NotImplementedException();
    }

    protected override bool IsValid(string value)
    {
        // Implement CPF validation logic here
        return string.IsNullOrWhiteSpace(value);
    }
}
