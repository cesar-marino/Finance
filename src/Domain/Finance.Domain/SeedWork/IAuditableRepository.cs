namespace Finance.Domain.SeedWork
{
    public interface IAuditableRepository<TAggregate> :
        IRepository<TAggregate> where TAggregate : AuditableAggregateRoot
    {
        Task<TAggregate> FindAsync(
            Guid id,
            Guid userId,
            CancellationToken cancellationToken = default);
    }
}