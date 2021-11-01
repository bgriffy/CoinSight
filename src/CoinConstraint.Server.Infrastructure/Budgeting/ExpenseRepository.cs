using CoinConstraint.Domain.AggregateModels.BudgetAggregate;
using CoinConstraint.Domain.AggregateModels.BudgetAggregate.Repositories;
using CoinConstraint.Server.Infrastructure.DataAccess;

namespace CoinConstraint.Server.Infrastructure.Budgets
{
    public class ExpenseRepository : Repository<Expense>, IExpenseRepository
    {
        private readonly CoinConstraintContext _context;

        public ExpenseRepository(CoinConstraintContext context) : base(context)
        {
            _context = context;
        }
    }
}
