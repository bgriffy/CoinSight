namespace CoinConstraint.Application.DataAccess;

public interface ICCUnitOfWork
{
    IBudgetRepository Budgets { get; set; }
    IExpenseRepository Expenses { get; set; }
    IReminderRepository Reminders { get; set; }
    INoteRepository Notes { get; }
}
