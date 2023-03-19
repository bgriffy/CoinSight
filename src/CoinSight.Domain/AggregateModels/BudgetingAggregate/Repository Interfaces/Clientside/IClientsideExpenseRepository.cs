namespace CoinConstraint.Domain.AggregateModels.BudgetAggregate.Repositories.Clientside;

public interface IClientsideExpenseRepository : IClientsideRepository<Expense>
{
    Task AddExpense(Expense expense);
    Task<List<Expense>> GetExpensesByBudget(int? budgetID);
    Task SaveChangesAsync();
    Task UpdateExpense(Expense expense);
}
