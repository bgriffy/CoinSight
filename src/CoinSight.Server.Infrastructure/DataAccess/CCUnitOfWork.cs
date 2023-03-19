using CoinConstraint.Domain.AggregateModels.BudgetingAggregate.Repository_Interfaces.Serverside;

namespace CoinConstraint.Server.Infrastructure.DataAccess;

public class CCUnitOfWork : ICCUnitOfWork
{
    private readonly CoinConstraintContext _context;

    public CCUnitOfWork(CoinConstraintContext context,
                        IBudgetRepository budgetRepository,
                        IExpenseRepository expenseRepository,
                        IReminderRepository reminderRepository,
                        INoteRepository noteRepository, 
                        IBudgetScheduleRepository budgetScheduleRepository)
    {
        _context = context;
        Budgets = budgetRepository;
        Expenses = expenseRepository;
        Reminders = reminderRepository;
        Notes = noteRepository;
        BudgetSchedules = budgetScheduleRepository;
    }

    public IBudgetRepository Budgets { get; set; }

    public IExpenseRepository Expenses { get; set; }

    public IReminderRepository Reminders { get; set; }

    public INoteRepository Notes { get; }
    public IBudgetScheduleRepository BudgetSchedules { get; }
}
