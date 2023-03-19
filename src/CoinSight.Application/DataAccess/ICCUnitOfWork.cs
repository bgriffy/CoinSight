using CoinConstraint.Domain.AggregateModels.BudgetingAggregate.Repository_Interfaces.Serverside;

namespace CoinConstraint.Application.DataAccess;

public interface ICCUnitOfWork
{
    IBudgetRepository Budgets { get; set; }
    IExpenseRepository Expenses { get; set; }
    IReminderRepository Reminders { get; set; }
    INoteRepository Notes { get; }
    IBudgetScheduleRepository BudgetSchedules { get; }
}
