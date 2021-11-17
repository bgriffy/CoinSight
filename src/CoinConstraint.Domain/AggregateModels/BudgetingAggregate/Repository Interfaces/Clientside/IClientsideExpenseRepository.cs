namespace CoinConstraint.Domain.AggregateModels.BudgetAggregate.Repositories.Clientside;

public interface IClientsideExpenseRepository : IClientsideRepository<Expense>
{
    Task<List<Expense>> GetExpensesByBudget(int budgetID);
    Task SaveChangesAsync();
}
