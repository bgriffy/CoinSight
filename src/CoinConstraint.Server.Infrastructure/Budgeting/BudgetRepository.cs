using CoinConstraint.Domain.AggregateModels.BudgetAggregate;
using CoinConstraint.Domain.AggregateModels.BudgetAggregate.Repositories;
using CoinConstraint.Server.Infrastructure.DataAccess;

namespace CoinConstraint.Server.Infrastructure.Budgets
{
    public class BudgetRepository : ServersideRepository<Budget>, IBudgetRepository
    {
        private readonly CoinConstraintContext _context;

        public BudgetRepository(CoinConstraintContext context) : base(context)
        {
            _context = context;
        }
    }
}
