using BankTransferApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankTransferApp.Infrastructure.EntityMapping;

public class UserMapping : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.ToTable("users");

        builder.HasKey(t => t.Id);

        builder.ComplexProperty(x => x.Name, x =>
        {
            x.Property(p => p.FirstName).HasColumnName("FirstName").HasMaxLength(100).HasColumnType("char").IsRequired();
            x.Property(p => p.LastName).HasColumnName("LastName").HasMaxLength(100).HasColumnType("char").IsRequired();
        });

        builder.ComplexProperty(x => x.CpfDocument, x =>
        {
            x.Property(p => p.Value).HasColumnName("Cpf").HasMaxLength(11).HasColumnType("char").IsRequired();
        });

        builder.ComplexProperty(x => x.Address, x =>
        {
            x.Property(p => p.Street).HasColumnName("Street").HasMaxLength(200).HasColumnType("char").IsRequired();
            x.Property(p => p.City).HasColumnName("City").HasMaxLength(100).HasColumnType("char").IsRequired();
            x.Property(p => p.State).HasColumnName("State").HasMaxLength(50).HasColumnType("char").IsRequired();
            x.Property(p => p.ZipCode).HasColumnName("ZipCode").HasMaxLength(20).HasColumnType("char").IsRequired();
        });

        builder.ComplexProperty(x => x.Cellphone, x =>
        {
            x.Property(p => p.AreaCode).HasColumnName("CellphoneAreaCode").HasMaxLength(3).HasColumnType("char").IsRequired();
            x.Property(p => p.Number).HasColumnName("CellphoneNumber").HasMaxLength(10).HasColumnType("char").IsRequired();
        });

        builder.ComplexProperty(x => x.HomePhone, x =>
        {
            x.Property(p => p.AreaCode).HasColumnName("HomePhoneAreaCode").HasMaxLength(3).HasColumnType("char").IsRequired();
            x.Property(p => p.Number).HasColumnName("HomePhoneNumber").HasMaxLength(10).HasColumnType("char").IsRequired();
        });

        builder.ComplexProperty(x => x.Password, x =>
        {
            x.Property(p => p.Value).HasColumnName("Password").HasColumnType("varchar").IsRequired();
        });
    }
}
