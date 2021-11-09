using CoinConstraint.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoinConstraint.Domain.AggregateModels.BudgetAggregate.Repositories
{
    public interface IExpenseRepository : IServersideRepository<Expense>
    {
        List<Expense> GetExpensesByBudget(int budgetID);
        Task SaveChangesAsync();
    }
}
