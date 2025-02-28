using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Terreiro.Domain.Entities;

namespace Terreiro.Persistence.Mappings;

public class UserRoleMapping : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.ToTable("user_roles");

        builder.HasKey(p => new { p.UserId, p.RoleId });

        builder.Property(p => p.RoleId)
            .HasColumnName("user_id")
            .IsRequired();

        builder.Property(p => p.UserId)
            .HasColumnName("role_id")
        .IsRequired();
    }
}
