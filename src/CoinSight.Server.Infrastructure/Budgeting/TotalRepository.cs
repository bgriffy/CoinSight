namespace CoinConstraint.Server.Infrastructure.Budgeting;

public class TotalRepository : ServersideRepository<Total>, ITotalRepository
{
    private readonly CoinConstraintContext _context;

    public TotalRepository(CoinConstraintContext context) : base(context)
    {
        _context = context;
    }
}
