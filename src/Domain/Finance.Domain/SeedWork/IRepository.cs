namespace Finance.Domain.SeedWork
{
    public interface IRepository<TAggregate>
    {
        // Task<TAggregate> FindAsync(Guid userId, Guid id, CancellationToken cancellationToken = default);
        Task InsertAsync(TAggregate aggregate, CancellationToken cancellationToken = default);
        Task UpdateAsync(TAggregate aggregate, CancellationToken cancellationToken = default);
    }
}
