using CoinConstraint.Domain.AggregateModels.BudgetingAggregate.Entities;

namespace CoinConstraint.Application.Budgeting;

public interface IBudgetingService
{
    void AddNewBudget(Budget budget);
    List<Budget> GetAllBudgets();
    Task Init();
    Task LoadBudgetData(Budget budget);
    void MarkBudgetForDeletion(Budget budget);
    void MarkExpenseForDeletion(Expense expense);
    Task SaveChanges(bool removeDeletedExpenses = false);
    Task SetSelectedBudget(Budget selectedBudget);
}
