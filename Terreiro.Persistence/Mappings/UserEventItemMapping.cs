using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Terreiro.Domain.Entities;

namespace Terreiro.Persistence.Mappings;

public class UserEventItemMapping : IEntityTypeConfiguration<UserEventItem>
{
    public void Configure(EntityTypeBuilder<UserEventItem> builder)
    {
        builder.ToTable("user_event_items");

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

        builder.Property(p => p.UserId)
            .HasColumnName("user_id")
            .IsRequired();

        builder.Property(p => p.EventItemId)
            .HasColumnName("event_item_id")
            .IsRequired();
    }
}
