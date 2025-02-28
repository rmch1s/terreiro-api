using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Terreiro.Domain.Entities;

namespace Terreiro.Persistence.Mappings;

public class UserEventMapping : IEntityTypeConfiguration<UserEvent>
{
    public void Configure(EntityTypeBuilder<UserEvent> builder)
    {
        builder.ToTable("user_events");

        builder.HasKey(p => new { p.UserId, p.EventId });

        builder.Property(p => p.UserId)
            .HasColumnName("user_id")
            .IsRequired();

        builder.Property(p => p.EventId)
            .HasColumnName("event_id")
            .IsRequired();
    }
}
