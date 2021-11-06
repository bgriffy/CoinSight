using CoinConstraint.Domain.Interfaces;
using System.Threading.Tasks;

namespace CoinConstraint.Domain.AggregateModels.BudgetAggregate.Repositories
{
    public interface IExpenseRepository : IRepository<Expense>
    {
        Task SaveChangesAsync();
    }
}
