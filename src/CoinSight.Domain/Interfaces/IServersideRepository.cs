namespace CoinConstraint.Domain.Interfaces;

public interface IServersideRepository<T> : IRepository<T>, IAsyncableRepository<T> where T : class
{
    Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate);
}
