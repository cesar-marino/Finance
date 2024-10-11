namespace Finance.Domain.SeedWork
{
    public abstract class AggregateRoot : EntityBase
    {
        protected AggregateRoot(
            Guid? id = null,
            DateTime? createdAt = null,
            DateTime? updatedAt = null) : base(id, createdAt, updatedAt)
        {
        }
    }
}
