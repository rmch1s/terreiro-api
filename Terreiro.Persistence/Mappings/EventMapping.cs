using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Terreiro.Domain.Entities;

namespace Terreiro.Persistence.Mappings;

public class EventMapping : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder.ToTable("events");

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

        builder.OwnsOne(p => p.Period, period =>
        {
            period.Property(p => p.StartDate)
            .IsRequired()
            .HasColumnName("start_date");

            period.Property(p => p.EndDate)
            .HasColumnName("end_date");
        });

        builder.HasMany(p => p.Users)
            .WithMany(u => u.Events)
            .UsingEntity<UserEvent>();
    }
}
