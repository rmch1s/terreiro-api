using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Terreiro.Domain.Entities;

namespace Terreiro.Persistence.Mappings;

public class EventItemMapping : IEntityTypeConfiguration<EventItem>
{
    public void Configure(EntityTypeBuilder<EventItem> builder)
    {
        builder.ToTable("event_items");

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
            .HasMaxLength(100);

        builder.Property(p => p.Quantity)
            .HasColumnName("quantity")
            .IsRequired();

        builder.Property(p => p.EventId)
            .HasColumnName("event_id")
            .IsRequired();

        builder.HasOne(p => p.Event)
            .WithMany(e => e.Items)
            .HasForeignKey(e => e.EventId);

        builder.HasMany(p => p.Users)
            .WithMany(u => u.EventItems)
            .UsingEntity<UserEventItem>();
    }
}
