using BankTransferApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankTransferApp.Infrastructure.EntityMapping;

public static class EFMappingsHelpers
{
    public static void MapAuditFields<T>(EntityTypeBuilder<T> builder)
        where T : class, IAuditedFields
    {
        builder.Property(x => x.CreatedById).IsRequired().HasColumnName("CreatedById");
        builder.Property(x => x.CreatedAt).IsRequired().HasColumnName("CreatedAt");
        builder.HasOne(x => x.CreatedBy).WithMany()
                    .HasForeignKey(x => x.CreatedById)
                    .OnDelete(DeleteBehavior.NoAction);

        builder.Property(x => x.ModifiedById).HasColumnName("ModifiedById");
        builder.Property(x => x.ModifiedAt).HasColumnName("ModifiedAt");
        builder.HasOne(x => x.ModifiedBy).WithMany()
                    .HasForeignKey(x => x.ModifiedById)
                    .OnDelete(DeleteBehavior.NoAction);

        builder.Property(x => x.DeletedById).HasColumnName("DeletedById");
        builder.Property(x => x.DeletedAt).HasColumnName("DeletedAt");
        builder.HasOne(x => x.DeletedBy).WithMany()
                    .HasForeignKey(x => x.DeletedById)
                    .OnDelete(DeleteBehavior.NoAction);

        builder.HasQueryFilter(x => x.DeletedAt == null);
    }

    public static void MapActivableFields<T>(EntityTypeBuilder<T> builder)
        where T : class, IActivableEntity
    {
        builder.Property(x => x.IsActive).IsRequired().HasColumnName("IsActive");
    }
}
