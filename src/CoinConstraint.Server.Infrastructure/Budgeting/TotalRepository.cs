using CoinConstraint.Domain.AggregateModels.BudgetAggregate;
using CoinConstraint.Domain.AggregateModels.BudgetAggregate.Repositories;
using CoinConstraint.Server.Infrastructure.DataAccess;

namespace CoinConstraint.Server.Infrastructure.Budgets
{
    public class TotalRepository : Repository<Total>, ITotalRepository
    {
        private readonly CoinConstraintContext _context;

        public TotalRepository(CoinConstraintContext context) : base(context)
        {
            _context = context;
        }
    }
}
