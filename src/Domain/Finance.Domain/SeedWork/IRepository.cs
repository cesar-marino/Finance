namespace Finance.Domain.SeedWork
{
    public interface IRepository<TAggregate> where TAggregate : AggregateRoot
    {
        Task<TAggregate> FindAsync(Guid id, CancellationToken cancellationToken = default);
        Task InsertAsync(TAggregate aggregate, CancellationToken cancellationToken = default);
        Task UpdateAsync(TAggregate aggregate, CancellationToken cancellationToken = default);
    }
}
