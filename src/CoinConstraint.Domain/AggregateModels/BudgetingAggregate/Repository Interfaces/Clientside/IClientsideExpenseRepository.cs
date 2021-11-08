using CoinConstraint.Domain.Interfaces;
using System.Threading.Tasks;

namespace CoinConstraint.Domain.AggregateModels.BudgetAggregate.Repositories.Clientside;

public interface IClientsideExpenseRepository : IClientsideRepository<Expense>
{
    Task SaveChangesAsync();
}
