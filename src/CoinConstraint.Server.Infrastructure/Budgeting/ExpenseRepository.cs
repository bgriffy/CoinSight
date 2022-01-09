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

    public bool ExpenseExists(int? expenseID, int? budgetID)
    {
        return _context.Expenses.Any(e => e.ID == expenseID && e.BudgetID == budgetID);
    }
}
