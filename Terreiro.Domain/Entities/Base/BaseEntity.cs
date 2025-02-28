namespace Terreiro.Domain.Entities.Base;

public abstract class BaseEntity : Entity
{
    public int Id { get; }
    public DateTime CreatedAt { get; } = DateTime.UtcNow;
    public DateTime? DeletedAt { get; private set; }

    public void SetDeletedAt() => DeletedAt = DateTime.UtcNow;
}
