namespace CoinConstraint.Server.Infrastructure.Budgeting;

public class BillRepository : ServersideRepository<Bill>, IBillRepository
{
    private readonly CoinConstraintContext _context;

    public BillRepository(CoinConstraintContext context) : base(context)
    {
        _context = context;
    }
}
