namespace CoinConstraint.Server.Infrastructure.DataAccess;

public class CCUnitOfWork : ICCUnitOfWork
{
    private readonly CoinConstraintContext _context;

    public CCUnitOfWork(CoinConstraintContext context,
                        IBillRepository billRepository,
                        IBudgetRepository budgetRepository,
                        IExpenseRepository expenseRepository,
                        IReminderRepository reminderRepository,
                        ITotalRepository totalRepository,
                        INoteRepository noteRepository)
    {
        _context = context;
        Bills = billRepository;
        Budgets = budgetRepository;
        Expenses = expenseRepository;
        Reminders = reminderRepository;
        Totals = totalRepository;
        Notes = noteRepository;
    }

    public IBillRepository Bills { get; set; }

    public IBudgetRepository Budgets { get; set; }

    public IExpenseRepository Expenses { get; set; }

    public IReminderRepository Reminders { get; set; }

    public ITotalRepository Totals { get; set; }

    public INoteRepository Notes { get; }

}
