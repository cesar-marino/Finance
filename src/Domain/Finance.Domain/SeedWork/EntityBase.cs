namespace Finance.Domain.SeedWork
{
    public abstract class EntityBase(
        Guid? id = null,
        DateTime? createdAt = null,
        DateTime? updatedAt = null)
    {
        public Guid Id { get; } = id ?? Guid.NewGuid();
        public DateTime CreatedAt { get; } = createdAt ?? DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = updatedAt ?? DateTime.UtcNow;
    }
}
