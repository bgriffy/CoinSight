using CoinConstraint.Domain.AggregateModels.BudgetAggregate;
using CoinConstraint.Domain.AggregateModels.BudgetAggregate.Repositories;
using CoinConstraint.Server.Infrastructure.DataAccess;

namespace CoinConstraint.Server.Infrastructure.Budgets
{
    public class BillRepository : ServersideRepository<Bill>, IBillRepository
    {
        private readonly CoinConstraintContext _context;

        public BillRepository(CoinConstraintContext context) : base(context)
        {
            _context = context;
        }
    }
}
