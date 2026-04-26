namespace BankTransferApp.Domain.Entities;

public interface IAuditedFields
{
    public DateTime CreatedAt { get; set; }
    public Guid CreatedById { get; set; }
    public UserEntity CreatedBy { get; set; }
    public DateTime? ModifiedAt { get; set; }
    public Guid? ModifiedById { get; set; }
    public UserEntity ModifiedBy { get; set; }
    public DateTime? DeletedAt { get; set; }
    public Guid? DeletedById { get; set; }
    public UserEntity DeletedBy { get; set; }
}

public static class AuditedFieldsExtensions
{
    public static void SetCreatedBy<T>(this T auditedFields, Guid userId)
        where T : IAuditedFields
    {
        auditedFields.CreatedById = userId;
        auditedFields.CreatedAt = DateTime.UtcNow;
    }
    public static void SetModifiedBy<T>(this T auditedFields, Guid userId)
        where T : IAuditedFields
    {
        auditedFields.ModifiedById = userId;
        auditedFields.ModifiedAt = DateTime.UtcNow;
    }
    public static void SetDeletedBy<T>(this T auditedFields, Guid userId)
        where T : IAuditedFields
    {
        auditedFields.DeletedById = userId;
        auditedFields.DeletedAt = DateTime.UtcNow;
    }
}