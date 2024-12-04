namespace Finance.Domain.SeedWork
{
    public abstract class AggregateRoot(
        Guid? id = null,
        DateTime? createdAt = null,
        DateTime? updatedAt = null) : EntityBase(
            id,
            createdAt,
            updatedAt)
    {
    }
}
