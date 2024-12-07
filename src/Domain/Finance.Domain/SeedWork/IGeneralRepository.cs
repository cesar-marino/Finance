namespace Finance.Domain.SeedWork
{
    public interface IGeneralRepository<TAggregate> :
        IRepository<TAggregate> where TAggregate : AggregateRoot
    {
        Task<TAggregate> FindAsync(Guid id, CancellationToken cancellationToken = default);
    }
}