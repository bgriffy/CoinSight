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
                var expenses = await _unitOfWork.Expenses.GetAllAsync();
                _expenses = expenses.OrderBy(e => e.DueDate).ToList();
                _expensesForDeletion = new List<Expense>();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public List<Expense> GetAllExpenses()
        {
            return _expenses;
        }

        public List<Expense> GetExpensesByBudget(int budgetID)
        {
            return _expenses.Where(e => e.BudgetID == budgetID).ToList();
        }

        public void MarkExpenseForDeletion(Expense expense)
        {
            _expensesForDeletion.Add(expense);
        }

        public async Task SaveChanges()
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
                Console.WriteLine($"There was an error saving changes from the budgeting service: {e.Message}");
                throw;
            }

            _expensesForDeletion.Clear();
        }
    }
}
