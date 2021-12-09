namespace CoinConstraint.Domain.AggregateModels.BudgetingAggregate.RepositoryInterfaces.Serverside;

public interface IBudgetRepository : IServersideRepository<Budget>
{
    Task<List<Budget>> GetBudgetsByUser(Guid userID);
}
