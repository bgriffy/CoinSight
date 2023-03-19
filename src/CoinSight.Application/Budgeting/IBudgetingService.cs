using CoinConstraint.Domain.AggregateModels.BudgetingAggregate.Entities;

namespace CoinConstraint.Application.Budgeting;

public interface IBudgetingService
{
    Task AddExpense(Expense expense);
    void AddNewBudget(Budget budget);
    List<Budget> GetAllBudgets();
    Task Init();
    void MarkBudgetForDeletion(Budget budget);
    void MarkExpenseForDeletion(Expense expense);
    void MarkNoteForDeletion(Note note);
    void MarkScheduleForDeletion(BudgetSchedule schedule);
    Task SaveBudget(Budget budget);
    Task SaveChanges(bool saveBudgetsOnly = true);
    Task SaveNewBudget(Budget budget);
    Task SetSelectedBudget(Budget selectedBudget);
    Task UpdateExpense(Expense expense);
}
