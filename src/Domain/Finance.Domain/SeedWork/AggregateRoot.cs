namespace Finance.Domain.SeedWork
{
    public abstract class AggregateRoot : EntityBase
    {
        protected AggregateRoot(
            Guid accountId,
            Guid? entityId = null,
            DateTime? createdAt = null,
            DateTime? updatedAt = null) : base(
                accountId,
                entityId,
                createdAt,
                updatedAt)
        {
        }
    }
}
