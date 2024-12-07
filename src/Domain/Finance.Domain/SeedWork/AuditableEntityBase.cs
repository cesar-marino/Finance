namespace Finance.Domain.SeedWork
{
    public abstract class AuditableEntityBase(
        Guid userId,
        Guid? id = null,
        DateTime? createdAt = null,
        DateTime? updatedAt = null) : EntityBase(id, createdAt, updatedAt)
    {
        public Guid UserId { get; } = userId;
    }
}