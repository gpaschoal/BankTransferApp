using BankTransferApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankTransferApp.Infrastructure.EntityMapping;

public class AccountMapping : IEntityTypeConfiguration<AccountEntity>
{
    public void Configure(EntityTypeBuilder<AccountEntity> builder)
    {
        builder.ToTable("accounts");

        builder.HasKey(t => t.Id);

        builder.HasOne(x => x.Owner).WithMany(x => x.Accounts)
            .HasForeignKey(x => x.OwnerId).OnDelete(DeleteBehavior.NoAction);

        builder.Property(x => x.AccountNumber).IsRequired().HasColumnName("AccountNumber").ValueGeneratedOnAdd();

        builder.Property(x => x.Balance).IsRequired().HasColumnName("Balance");

        builder.Property(x => x.AccountType).IsRequired().HasColumnName("AccountType").HasColumnType("int");

        EFMappingsHelpers.MapAuditFields(builder);

        EFMappingsHelpers.MapActivableFields(builder);
    }
}