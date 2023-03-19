namespace CoinConstraint.Domain.AggregateModels.BudgetAggregate.Repositories.Clientside;

public interface IClientsideBudgetRepository : IClientsideRepository<Budget>
{
    Task<int?> AddBudget(Budget budget);
    Task<List<Budget>> GetBudgetsByUser(Guid userID);
    Task<List<Budget>> GetCurrentUserBudgets();
}
