namespace CoinConstraint.Server.Infrastructure.Budgeting;

public class ExpenseRepository : ServersideRepository<Expense>, IExpenseRepository
{
    private readonly CoinConstraintContext _context;

    public ExpenseRepository(CoinConstraintContext context) : base(context)
    {
        _context = context;
    }

    public List<Expense> GetExpensesByBudget(int budgetID)
    {
        return _context.Expenses.Where(e => e.BudgetID == budgetID).ToList();
    }
}
