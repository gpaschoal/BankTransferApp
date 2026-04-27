using BankTransferApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankTransferApp.Infrastructure.EntityMapping;

public class TransferMapping : IEntityTypeConfiguration<TransferEntity>
{
    public void Configure(EntityTypeBuilder<TransferEntity> builder)
    {
        builder.ToTable("transfers");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Value).IsRequired().HasColumnName("Value");

        builder.Property(x => x.Reference).IsRequired().HasColumnName("Reference");

        builder.Property(x => x.SourceAccountId).IsRequired().HasColumnName("SourceAccountId");
        builder.HasOne(x => x.SourceAccount).WithMany()
            .HasForeignKey(x => x.SourceAccountId).OnDelete(DeleteBehavior.NoAction);

        builder.Property(x => x.DestinationAccountId).IsRequired().HasColumnName("DestinationAccountId");
        builder.HasOne(x => x.DestinationAccount).WithMany()
            .HasForeignKey(x => x.DestinationAccountId).OnDelete(DeleteBehavior.NoAction);
    }
}