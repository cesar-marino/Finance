namespace Finance.Domain.SeedWork
{
    internal interface IRepository<TAggregate>
    {
        Task InsertAsync(TAggregate aggregate, CancellationToken cancellationToken = default);
        Task UpdateAsync(TAggregate aggregate, CancellationToken cancellationToken = default);
    }
}
