namespace BankTransferApp.Domain.ValueObjects;

public record AddressValueObject
{
    public AddressValueObject(string street, string city, string state, string zipCode)
    {
        Street = street;
        City = city;
        State = state;
        ZipCode = zipCode;
    }

    // EF Core requires a parameterless constructor for value objects
    public AddressValueObject() { }

    public string Street { get; }
    public string City { get; }
    public string State { get; }
    public string ZipCode { get; }
}