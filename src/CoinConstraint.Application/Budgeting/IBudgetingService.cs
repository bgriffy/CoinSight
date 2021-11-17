using CoinConstraint.Domain.AggregateModels.BudgetingAggregate.Entities;

namespace CoinConstraint.Application.Budgeting;

public interface IBudgetingService
{
    void AddNewBudget(Budget budget);
    List<Budget> GetAllBudgets();
    List<Expense> GetExpenses();
    Task<List<Expense>> GetExpensesByBudget(int budgetID);
    Task Init();
    void MarkBudgetForDeletion(Budget budget);
    void MarkExpenseForDeletion(Expense expense);
    Task SaveBudgets();
    Task SaveChanges();
    Task SetSelectedBudget(Budget selectedBudget);
}
