using CoinConstraint.Domain.AggregateModels.BudgetAggregate;
using CoinConstraint.Domain.AggregateModels.BudgetAggregate.Repositories;
using CoinConstraint.Server.Infrastructure.DataAccess;

namespace CoinConstraint.Server.Infrastructure.Budgets
{
    public class ReminderRepository : Repository<Reminder>, IReminderRepository
    {
        private readonly CoinConstraintContext _context;

        public ReminderRepository(CoinConstraintContext context) : base(context)
        {
            _context = context;
        }
    }
}
