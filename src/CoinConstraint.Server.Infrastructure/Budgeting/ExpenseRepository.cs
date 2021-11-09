using CoinConstraint.Domain.AggregateModels.BudgetAggregate;
using CoinConstraint.Domain.AggregateModels.BudgetAggregate.Repositories;
using CoinConstraint.Server.Infrastructure.DataAccess;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoinConstraint.Server.Infrastructure.Budgets
{
    public class ExpenseRepository : ServersideRepository<Expense>, IExpenseRepository
    {
        private readonly CoinConstraintContext _context;

        public ExpenseRepository(CoinConstraintContext context) : base(context)
        {
            _context = context;
        }

        public List<Expense> GetExpensesByBudget(int budgetID)
        {
            return _context.Expenses.Where(e => e.BudgetID == budgetID).ToList();
        }
    }
}
