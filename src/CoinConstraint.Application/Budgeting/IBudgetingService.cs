using CoinConstraint.Domain.AggregateModels.BudgetAggregate;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoinConstraint.Application.Budgeting
{
    public interface IBudgetingService
    {
        List<Expense> GetAllExpenses();
        List<Expense> GetExpensesByBudget(int budgetID);
        Task Init();
        Task SaveChanges();
    }
}