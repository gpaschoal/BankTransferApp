using BankTransferApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankTransferApp.Infrastructure.EntityMapping;

public class TransactionMapping : IEntityTypeConfiguration<TransactionEntity>
{
    public void Configure(EntityTypeBuilder<TransactionEntity> builder)
    {
        builder.ToTable("transactions");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Value).IsRequired().HasColumnName("Value");

        builder.Property(x => x.Reference).IsRequired().HasColumnName("Reference");

        builder.Property(x => x.Type).IsRequired().HasColumnName("Type").HasColumnType("int");

        builder.Property(x => x.AccountId).IsRequired().HasColumnName("AccountId");
        builder.HasOne(x => x.Account).WithMany()
            .HasForeignKey(x => x.AccountId).OnDelete(DeleteBehavior.NoAction);

        builder.Property(x => x.BalanceId).HasColumnName("BalanceId");
        builder.HasOne(x => x.Balance).WithMany()
            .HasForeignKey(x => x.BalanceId).OnDelete(DeleteBehavior.NoAction);
    }
}