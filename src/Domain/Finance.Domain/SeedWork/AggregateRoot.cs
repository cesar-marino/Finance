namespace Finance.Domain.SeedWork
{
    public abstract class AggregateRoot : EntityBase
    {
        protected AggregateRoot(
            Guid accountId,
            Guid? id = null,
            DateTime? createdAt = null,
            DateTime? updatedAt = null) : base(
                accountId: accountId,
                id: id,
                createdAt: createdAt,
                updatedAt: updatedAt)
        {
        }
    }
}
