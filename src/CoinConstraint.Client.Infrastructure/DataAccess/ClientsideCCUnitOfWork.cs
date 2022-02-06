using CoinConstraint.Domain.AggregateModels.BudgetingAggregate.Repository_Interfaces.Clientside;

namespace CoinConstraint.Client.Infrastructure.DataAccess;

public class ClientsideCCUnitOfWork : IClientsideCCUnitOfWork
{
    public ClientsideCCUnitOfWork(IClientsideBudgetRepository budgetRepository,
                                  IClientsideExpenseRepository expenseRepository,
                                  IClientsideReminderRepository reminderRepository,
                                  IClientsideNoteRepository noteRepository, 
                                  IClientsideBudgetScheduleRepository budgetScheduleRepository)
    {
        Budgets = budgetRepository;
        Expenses = expenseRepository;
        Reminders = reminderRepository;
        Notes = noteRepository;
        BudgetSchedules = budgetScheduleRepository;
    }


    public IClientsideBudgetRepository Budgets { get; set; }

    public IClientsideExpenseRepository Expenses { get; set; }

    public IClientsideReminderRepository Reminders { get; set; }

    public IClientsideNoteRepository Notes { get; }

    public IClientsideBudgetScheduleRepository BudgetSchedules { get; }

}
