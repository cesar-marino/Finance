namespace Finance.Domain.SeedWork
{
    public abstract class EntityBase
    {
        public Guid AccountId { get; }
        public Guid EntityId { get; }
        public DateTime CreatedAt { get; }
        public DateTime UpdatedAt { get; set; }

        protected EntityBase(
            Guid accountId,
            Guid? entityId = null,
            DateTime? createdAt = null,
            DateTime? updatedAt = null)
        {
            AccountId = accountId;
            EntityId = entityId ?? Guid.NewGuid();
            CreatedAt = createdAt ?? DateTime.UtcNow;
            UpdatedAt = updatedAt ?? DateTime.UtcNow;
        }
    }
}
