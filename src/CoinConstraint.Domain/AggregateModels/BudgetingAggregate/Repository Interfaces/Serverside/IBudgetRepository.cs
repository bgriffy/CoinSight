namespace CoinConstraint.Domain.AggregateModels.BudgetingAggregate.RepositoryInterfaces.Serverside;

public interface IBudgetRepository : IServersideRepository<Budget>
{
    bool BudgetExists(int? budgetID);
    bool BudgetExists(int? budgetID, string uuid);
    Budget GetBudgetByID(int? id);
    Task<List<Budget>> GetBudgetsByUser(Guid userID);
}