using CoinConstraint.Domain.AggregateModels.BudgetingAggregate.Entities;

namespace CoinConstraint.Application.Budgeting;

public interface IBudgetingService
{
    void AddNewBudget(Budget budget);
    List<Budget> GetAllBudgets();
    Task Init();
    void MarkBudgetForDeletion(Budget budget);
    void MarkExpenseForDeletion(Expense expense);
    void MarkNoteForDeletion(Note note);
    Task SaveChanges(bool saveBudgetsOnly = true);
    Task SetSelectedBudget(Budget selectedBudget);
}
