namespace BankTransferApp.Domain.ValueObjects;

public record TelephoneValueObject
{
    public string AreaCode { get; }
    public string Number { get; }

    public TelephoneValueObject(string areaCode, string number)
    {
        AreaCode = areaCode;
        Number = number;
    }

    // EF Core requires a parameterless constructor for value objects
    public TelephoneValueObject() { }
}
