using BankTransferApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankTransferApp.Infrastructure.EntityMapping;

public class WithdrawalMapping : IEntityTypeConfiguration<WithdrawalEntity>
{
    public void Configure(EntityTypeBuilder<WithdrawalEntity> builder)
    {
        builder.ToTable("withdrawals");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Value).IsRequired().HasColumnName("Value");

        builder.Property(x => x.Reference).IsRequired().HasColumnName("Reference");

        builder.Property(x => x.AccountId).IsRequired().HasColumnName("AccountId");
        builder.HasOne(x => x.Account).WithMany()
            .HasForeignKey(x => x.AccountId).OnDelete(DeleteBehavior.NoAction);

        builder.Property(x => x.TransactionId).IsRequired().HasColumnName("TransactionId");
        builder.HasOne(x => x.Transaction).WithMany()
            .HasForeignKey(x => x.TransactionId).OnDelete(DeleteBehavior.NoAction);
    }
}