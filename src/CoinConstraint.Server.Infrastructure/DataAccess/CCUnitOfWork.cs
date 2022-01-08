namespace CoinConstraint.Server.Infrastructure.DataAccess;

public class CCUnitOfWork : ICCUnitOfWork
{
    private readonly CoinConstraintContext _context;

    public CCUnitOfWork(CoinConstraintContext context,
                        IBudgetRepository budgetRepository,
                        IExpenseRepository expenseRepository,
                        IReminderRepository reminderRepository,
                        INoteRepository noteRepository)
    {
        _context = context;
        Budgets = budgetRepository;
        Expenses = expenseRepository;
        Reminders = reminderRepository;
        Notes = noteRepository;
    }

    public IBudgetRepository Budgets { get; set; }

    public IExpenseRepository Expenses { get; set; }

    public IReminderRepository Reminders { get; set; }

    public INoteRepository Notes { get; }

}
