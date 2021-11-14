using CoinConstraint.Domain.AggregateModels.BudgetAggregate;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoinConstraint.Application.Budgeting
{
    public interface IBudgetingService
    {
        void AddNewBudget(Budget budget);
        List<Budget> GetAllBudgets();
        List<Expense> GetExpenses();
        Task<List<Expense>> GetExpensesByBudget(int budgetID);
        Task Init();
        void MarkExpenseForDeletion(Expense expense);
        Task SaveBudgets();
        Task SaveChanges();
        Task SetSelectedBudget(Budget selectedBudget);
    }
}