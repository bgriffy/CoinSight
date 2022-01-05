using CoinConstraint.Domain.AggregateModels.BudgetingAggregate.RepositoryInterfaces.Clientside;

namespace CoinConstraint.Application.DataAccess;

public interface IClientsideCCUnitOfWork
{
    IClientsideBillRepository Bills { get; set; }
    IClientsideBudgetRepository Budgets { get; set; }
    IClientsideExpenseRepository Expenses { get; set; }
    IClientsideReminderRepository Reminders { get; set; }
    IClientsideTotalRepository Totals { get; set; }
    IClientsideNoteRepository Notes { get; }
}
