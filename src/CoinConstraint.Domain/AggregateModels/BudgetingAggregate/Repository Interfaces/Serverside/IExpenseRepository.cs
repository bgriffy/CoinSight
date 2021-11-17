namespace CoinConstraint.Domain.AggregateModels.BudgetingAggregate.RepositoryInterfaces.Serverside;

public interface IExpenseRepository : IServersideRepository<Expense>
{
    List<Expense> GetExpensesByBudget(int budgetID);
    Task SaveChangesAsync();
}
