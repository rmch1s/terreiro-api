using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Terreiro.Domain.Entities;

namespace Terreiro.Persistence.Mappings;

public class UserMapping : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");

        builder.HasKey(t => t.Id);

        builder.Property(p => p.Id)
            .HasColumnName("id")
            .IsRequired()
            .UseSequence();

        builder.Property(p => p.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder.Property(p => p.DeletedAt)
            .HasColumnName("deleted_at");

        builder.Property(p => p.Name)
            .HasColumnName("name")
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(p => p.CPF)
            .HasColumnName("cpf")
            .IsRequired()
            .HasMaxLength(11);

        builder.Property(p => p.PIN)
            .HasColumnName("pin")
            .HasMaxLength(4);

        builder.OwnsOne(p => p.Cellphone, cellphone =>
        {
            cellphone.Property(p => p.DDD)
            .HasColumnName("phone_ddd")
            .IsRequired()
            .HasMaxLength(2);

            cellphone.Property(p => p.Number)
            .HasColumnName("phone_number")
            .IsRequired()
            .HasMaxLength(9);
        });
    }
}
