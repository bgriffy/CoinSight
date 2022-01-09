namespace CoinConstraint.Domain.AggregateModels.BudgetingAggregate.RepositoryInterfaces.Serverside;

public interface IExpenseRepository : IServersideRepository<Expense>
{
    bool ExpenseExists(int? expenseID, int? budgetID);
    List<Expense> GetExpensesByBudget(int budgetID);
    Task SaveChangesAsync();
}
