namespace CoinConstraint.Server.Infrastructure.Budgeting;

public class BudgetRepository : ServersideRepository<Budget>, IBudgetRepository
{
    private readonly CoinConstraintContext _context;

    public BudgetRepository(CoinConstraintContext context) : base(context)
    {
        _context = context;
    }

    public Budget GetBudgetByID(int? id)
    {
        return _context.Budgets.FirstOrDefault(b => b.ID == id);
    }

    public async Task<List<Budget>> GetBudgetsByUser(Guid userID)
    {
        return await _context.Budgets.Where(e => e.UUID == userID).ToListAsync();
    }

    public bool BudgetExists(int? budgetID)
    {
        return _context.Budgets.Any(e => e.ID == budgetID);
    }

    public bool BudgetExists(int? budgetID, string uuid)
    {
        var guid = Guid.Parse(uuid);
        return _context.Budgets.Any(e => e.UUID == guid && e.ID == budgetID);
    }
}
