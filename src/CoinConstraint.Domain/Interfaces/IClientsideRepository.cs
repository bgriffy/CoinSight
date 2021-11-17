namespace CoinConstraint.Domain.Interfaces;

public interface IClientsideRepository<T> : IAsyncableRepository<T> where T : class
{
    Task UpdateAsync(T entity);
    Task RemoveAsync(T entity);
    Task RemoveRangeAsync(IEnumerable<T> entities);
    Task RemoveAllAsync();
}
