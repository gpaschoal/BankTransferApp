using BankTransferApp.Domain.ValueObjects;

namespace BankTransferApp.Domain.Entities;

public class UserEntity : IEntity
{
    public Guid Id { get; set; }
    public PersonNameValueObject Name { get; set; }
    public CpfValueObject CpfDocument { get; set; }
    public AddressValueObject Address { get; set; }
    public TelephoneValueObject Cellphone { get; set; }
    public TelephoneValueObject HomePhone { get; set; }

    public static UserEntity Create(
        PersonNameValueObject name,
        CpfValueObject cpfDocument,
        AddressValueObject address,
        TelephoneValueObject cellphone,
        TelephoneValueObject homePhone)
    {
        return new UserEntity
        {
            Id = Guid.CreateVersion7(),
            Name = name,
            CpfDocument = cpfDocument,
            Address = address,
            Cellphone = cellphone,
            HomePhone = homePhone
        };
    }
}
