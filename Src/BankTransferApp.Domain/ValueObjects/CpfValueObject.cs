namespace BankTransferApp.Domain.ValueObjects;

public class CpfValueObject : AbstractDocumentValueObject
{
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

    private bool IsValid(string cpf) { return true; }
}
