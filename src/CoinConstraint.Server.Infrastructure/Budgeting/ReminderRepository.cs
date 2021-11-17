namespace CoinConstraint.Server.Infrastructure.Budgeting;

public class ReminderRepository : ServersideRepository<Reminder>, IReminderRepository
{
    private readonly CoinConstraintContext _context;

    public ReminderRepository(CoinConstraintContext context) : base(context)
    {
        _context = context;
    }
}
