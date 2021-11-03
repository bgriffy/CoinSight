using CoinConstraint.Domain.AggregateModels.BudgetAggregate.Repositories;
using CoinConstraint.Domain.AggregateModels.UserAggregate;
using CoinConstraint.Server.Infrastructure.DataAccess;

namespace CoinConstraint.Server.Infrastructure.Budgets
{
    public class UserRepository : ServersideRepository<User>, IUserRepository
    {
        private readonly CoinConstraintContext _context;

        public UserRepository(CoinConstraintContext context) : base(context)
        {
            _context = context;
        }
    }
}
