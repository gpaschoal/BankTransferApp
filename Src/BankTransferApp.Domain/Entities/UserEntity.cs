using BankTransferApp.Domain.ValueObjects;

namespace BankTransferApp.Domain.Entities;

public class UserEntity : IEntity, IAuditedFields, IActivableEntity
{
    public Guid Id { get; set; }
    public PersonNameValueObject Name { get; set; }
    public CpfValueObject CpfDocument { get; set; }
    public AddressValueObject Address { get; set; }
    public TelephoneValueObject Cellphone { get; set; }
    public TelephoneValueObject HomePhone { get; set; }
    public PasswordValueObject Password { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid CreatedById { get; set; }
    public UserEntity CreatedBy { get; set; }
    public DateTime? ModifiedAt { get; set; }
    public Guid? ModifiedById { get; set; }
    public UserEntity ModifiedBy { get; set; }
    public DateTime? DeletedAt { get; set; }
    public Guid? DeletedById { get; set; }
    public UserEntity DeletedBy { get; set; }
    public bool IsActive { get; set; }
    public ICollection<AccountEntity> Accounts { get; set; } = [];

    public static UserEntity Create(
        PersonNameValueObject name,
        CpfValueObject cpfDocument,
        AddressValueObject address,
        TelephoneValueObject cellphone,
        TelephoneValueObject homePhone,
        PasswordValueObject password)
    {
        var id = Guid.NewGuid();
        UserEntity userEntity = new()
        {
            Id = id,
            Name = name,
            CpfDocument = cpfDocument,
            Address = address,
            Cellphone = cellphone,
            HomePhone = homePhone,
            Password = password,
            IsActive = true
        };

        userEntity.SetCreatedBy(id);

        return userEntity;
    }
}
