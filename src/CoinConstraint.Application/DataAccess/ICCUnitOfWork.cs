namespace CoinConstraint.Application.DataAccess;

public interface ICCUnitOfWork
{
    IBillRepository Bills { get; set; }
    IBudgetRepository Budgets { get; set; }
    IExpenseRepository Expenses { get; set; }
    IReminderRepository Reminders { get; set; }
    ITotalRepository Totals { get; set; }
    IUserRepository Users { get; }
}
