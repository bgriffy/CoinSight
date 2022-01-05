namespace CoinConstraint.Client.Infrastructure.DataAccess;

public class ClientsideCCUnitOfWork : IClientsideCCUnitOfWork
{
    public ClientsideCCUnitOfWork(IClientsideBillRepository billRepository,
                                  IClientsideBudgetRepository budgetRepository,
                                  IClientsideExpenseRepository expenseRepository,
                                  IClientsideReminderRepository reminderRepository,
                                  IClientsideTotalRepository totalRepository,
                                  IClientsideNoteRepository noteRepository)
    {
        Bills = billRepository;
        Budgets = budgetRepository;
        Expenses = expenseRepository;
        Reminders = reminderRepository;
        Totals = totalRepository;
        Notes = noteRepository;
    }

    public IClientsideBillRepository Bills { get; set; }

    public IClientsideBudgetRepository Budgets { get; set; }

    public IClientsideExpenseRepository Expenses { get; set; }

    public IClientsideReminderRepository Reminders { get; set; }

    public IClientsideTotalRepository Totals { get; set; }

    public IClientsideNoteRepository Notes { get; }

}
