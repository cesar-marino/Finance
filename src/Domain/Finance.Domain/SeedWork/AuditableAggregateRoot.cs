namespace Finance.Domain.SeedWork
{
    public abstract class AuditableAggregateRoot(
        Guid userId,
        Guid? id = null,
        DateTime? createdAt = null,
        DateTime? updatedAt = null) : AuditableEntityBase(id: id, userId: userId, createdAt: createdAt, updatedAt: updatedAt)
    {
    }
}