using BankTransferApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankTransferApp.Infrastructure.EntityMapping;

public class BalancePerMonthMapping : IEntityTypeConfiguration<BalancePerMonthEntity>
{
    public void Configure(EntityTypeBuilder<BalancePerMonthEntity> builder)
    {
        builder.ToTable("balance_per_month");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Balance).IsRequired().HasColumnName("Balance");

        builder.Property(x => x.Year).IsRequired().HasColumnName("Year");

        builder.Property(x => x.Month).IsRequired().HasColumnName("Month");

        builder.Property(x => x.AccountId).IsRequired().HasColumnName("AccountId");
        builder.HasOne(x => x.Account).WithMany()
            .HasForeignKey(x => x.AccountId).OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(x => x.Transactions).WithOne(x => x.Balance)
            .HasForeignKey(x => x.BalanceId).OnDelete(DeleteBehavior.NoAction);
    }
}