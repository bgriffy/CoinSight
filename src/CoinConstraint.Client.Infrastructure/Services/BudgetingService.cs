using CoinConstraint.Application.Budgeting;
using CoinConstraint.Application.DataAccess;
using CoinConstraint.Domain.AggregateModels.BudgetAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoinConstraint.Client.Infrastructure.Services
{
    public class BudgetingService : IBudgetingService
    {
        private readonly IClientsideCCUnitOfWork _unitOfWork;
        private Budget _selectedBudget;
        private List<Budget> _budgets; 
        private List<Expense> _expenses;
        private List<Expense> _expensesForDeletion;

        public BudgetingService(IClientsideCCUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Init()
        {
            try
            {
                _budgets = (List<Budget>)await _unitOfWork.Budgets.GetAllAsync();
                _selectedBudget = _budgets.FirstOrDefault();
                _expensesForDeletion = new List<Expense>();
                _expenses = new List<Expense>();
                if (_selectedBudget != null)
                {
                    await SetExpenses(_selectedBudget.ID);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public async Task SetSelectedBudget(Budget selectedBudget)
        {
            _selectedBudget = selectedBudget;
            await SetExpenses(_selectedBudget.ID);
        }

        public List<Budget> GetAllBudgets()
        {
            return _budgets;
        }

        public List<Expense> GetExpenses()
        {
            return _expenses;
        }

        public async Task<List<Expense>> GetExpensesByBudget(int budgetID)
        {
            await SetExpenses(budgetID);
            return _expenses;
        }

        public async Task SetExpenses(int budgetID)
        {
            _expenses = await _unitOfWork.Expenses.GetExpensesByBudget(budgetID);
        }

        public void MarkExpenseForDeletion(Expense expense)
        {
            _expensesForDeletion.Add(expense);
        }

        public void AddNewBudget(Budget budget)
        {
            _budgets.Add(budget);
        }

        public async Task SaveChanges()
        {
            try
            {
                await SaveBudgets();
                await SaveExpenses();
            }
            catch (Exception e)
            {
                Console.WriteLine($"There was an error saving changes from the budgeting service: {e.Message}");
                throw;
            }
        }

        private async Task SaveBudgets()
        {
            try
            {
                foreach (var budget in _budgets)
                {
                    if (budget.IsNew)
                    {
                        await _unitOfWork.Budgets.AddAsync(budget);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"There was an error saving budgets from the budgeting service: {e.Message}");
                throw;
            }
        }

        private async Task SaveExpenses()
        {
            try
            {
                foreach (var expense in _expenses)
                {
                    if (expense.IsNew)
                    {
                        await _unitOfWork.Expenses.AddAsync(expense);
                    }
                    else if (expense.IsUpdated)
                    {
                        await _unitOfWork.Expenses.UpdateAsync(expense);
                    }
                }

                //await _unitOfWork.Expenses.RemoveRangeAsync(_expensesForDeletion);

                foreach (var expense in _expensesForDeletion)
                {
                    await _unitOfWork.Expenses.RemoveAsync(expense);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"There was an error saving expenses from the budgeting service: {e.Message}");
                throw;
            }

            _expensesForDeletion.Clear();
        }
    }
}
