namespace CoinConstraint.Server.Infrastructure.Budgeting;

public class BudgetRepository : ServersideRepository<Budget>, IBudgetRepository
{
    private readonly CoinConstraintContext _context;

    public BudgetRepository(CoinConstraintContext context) : base(context)
    {
        _context = context;
    }
}
