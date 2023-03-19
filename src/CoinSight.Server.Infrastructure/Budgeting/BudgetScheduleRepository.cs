using CoinConstraint.Domain.AggregateModels.BudgetingAggregate.Repository_Interfaces.Serverside;

namespace CoinConstraint.Server.Infrastructure.Budgeting;

public class BudgetScheduleRepository : ServersideRepository<BudgetSchedule>, IBudgetScheduleRepository
{
    private readonly CoinConstraintContext _context;

    public BudgetScheduleRepository(CoinConstraintContext context) : base(context)
    {
        _context = context;
    }
}
