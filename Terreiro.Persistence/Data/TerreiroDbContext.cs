using Microsoft.EntityFrameworkCore;
using Terreiro.Domain.Entities;
using Terreiro.Persistence.Mappings;

namespace Terreiro.Persistence.Data;

public class TerreiroDbContext(DbContextOptions<TerreiroDbContext> options) : DbContext(options)
{
    public required DbSet<User> Users { get; set; }
    public required DbSet<UserEvent> UserEvents { get; set; }
    public required DbSet<UserRole> UserRoles { get; set; }
    public required DbSet<UserEventItem> UserEventItems { get; set; }
    public required DbSet<Role> Roles { get; set; }
    public required DbSet<Event> Events { get; set; }
    public required DbSet<EventItem> EventItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserMapping());
        modelBuilder.ApplyConfiguration(new UserRoleMapping());
        modelBuilder.ApplyConfiguration(new UserEventItemMapping());
        modelBuilder.ApplyConfiguration(new RoleMapping());
        modelBuilder.ApplyConfiguration(new EventMapping());
        modelBuilder.ApplyConfiguration(new EventItemMapping());
        modelBuilder.ApplyConfiguration(new UserEventMapping());
    }
}
