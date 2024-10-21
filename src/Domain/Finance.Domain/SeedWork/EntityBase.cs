namespace Finance.Domain.SeedWork
{
    public abstract class EntityBase
    {
        public Guid AccountId { get; }
        public Guid Id { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; set; }

        protected EntityBase(
            Guid accountId,
            Guid? id = null,
            DateTime? createdAt = null,
            DateTime? updatedAt = null)
        {
            AccountId = accountId;
            Id = id ?? Guid.NewGuid();
            CreatedAt = createdAt ?? DateTime.UtcNow;
            UpdatedAt = updatedAt ?? DateTime.UtcNow;
        }
    }
}
