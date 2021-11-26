using CoinConstraint.Domain.AggregateModels.BudgetingAggregate.Entities;

namespace CoinConstraint.Application.Budgeting;

public interface IBudgetingService
{
    void AddNewBudget(Budget budget);
    Budget CloneBudget(Budget budget);
    List<Budget> GetAllBudgets();
    Task Init();
    void MarkBudgetForDeletion(Budget budget);
    void MarkExpenseForDeletion(Expense expense);
    Task SaveBudgets();
    Task SaveChanges();
    Task SetSelectedBudget(Budget selectedBudget);
}
