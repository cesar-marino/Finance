namespace Finance.Domain.SeedWork
{
    public abstract class AggregateRoot(
        Guid? entityId = null,
        DateTime? createdAt = null,
        DateTime? updatedAt = null) : EntityBase(
            entityId,
            createdAt,
            updatedAt)
    {
    }
}
