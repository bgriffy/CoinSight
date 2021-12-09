namespace CoinConstraint.Server.Infrastructure.Budgeting;

public class BudgetRepository : ServersideRepository<Budget>, IBudgetRepository
{
    private readonly CoinConstraintContext _context;

    public BudgetRepository(CoinConstraintContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<Budget>> GetBudgetsByUser(Guid userID)
    {
        return await _context.Budgets.Where(e => e.UUID == userID).ToListAsync();
    }
}
