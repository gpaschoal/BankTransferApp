namespace BankTransferApp.Domain.ValueObjects;

public record PersonNameValueObject
{
    public PersonNameValueObject(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    // EF Core requires a parameterless constructor for value objects
    public PersonNameValueObject() { }

    public string FullName => $"{FirstName.Trim()} {LastName.Trim()}";

    public string FirstName { get; }
    public string LastName { get; }
}