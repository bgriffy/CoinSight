using CoinConstraint.Domain.AggregateModels.BudgetAggregate.Repositories.Client;
using CoinConstraint.Domain.AggregateModels.BudgetAggregate.Repositories.Clientside;

namespace CoinConstraint.Application.DataAccess
{
    public interface IClientsideCCUnitOfWork
    {
        IClientsideBillRepository Bills { get; set; }
        IClientsideBudgetRepository Budgets { get; set; }
        IClientsideExpenseRepository Expenses { get; set; }
        IClientsideReminderRepository Reminders { get; set; }
        IClientsideTotalRepository Totals { get; set; }
        IClientsideUserRepository Users { get; }
    }
}
