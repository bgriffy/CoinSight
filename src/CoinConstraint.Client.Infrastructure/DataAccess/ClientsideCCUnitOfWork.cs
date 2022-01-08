namespace CoinConstraint.Client.Infrastructure.DataAccess;

public class ClientsideCCUnitOfWork : IClientsideCCUnitOfWork
{
    public ClientsideCCUnitOfWork(IClientsideBudgetRepository budgetRepository,
                                  IClientsideExpenseRepository expenseRepository,
                                  IClientsideReminderRepository reminderRepository,
                                  IClientsideNoteRepository noteRepository)
    {
        Budgets = budgetRepository;
        Expenses = expenseRepository;
        Reminders = reminderRepository;
        Notes = noteRepository;
    }


    public IClientsideBudgetRepository Budgets { get; set; }

    public IClientsideExpenseRepository Expenses { get; set; }

    public IClientsideReminderRepository Reminders { get; set; }

    public IClientsideNoteRepository Notes { get; }

}
