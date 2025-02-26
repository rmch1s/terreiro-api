using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Terreiro.Domain.Entities;

namespace Terreiro.Persistence.Mappings;

public class RoleMapping : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("roles");

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
            .IsRequired()
            .HasColumnName("name")
            .HasMaxLength(100);

        builder.Property(p => p.Description)
            .HasColumnName("description")
            .HasMaxLength(300);

        builder.HasMany(p => p.Users)
            .WithMany(u => u.Roles)
            .UsingEntity<UserRole>();
    }
}
