using CoinConstraint.Domain.AggregateModels.BudgetingAggregate.Entities;

namespace CoinConstraint.Client.Infrastructure.Services;

public class BudgetingService : IBudgetingService
{
    private readonly IClientsideCCUnitOfWork _unitOfWork;
    private Budget _selectedBudget;
    private List<Budget> _budgets;
    private List<Budget> _budgetsForDeletion;
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
            _budgetsForDeletion = new List<Budget>();
            _selectedBudget = _budgets.FirstOrDefault();
            _expensesForDeletion = new List<Expense>();
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
        return _selectedBudget.Expenses;
    }

    public async Task<List<Expense>> GetExpensesByBudget(int budgetID)
    {
        await SetExpenses(budgetID);
        return _selectedBudget.Expenses;
    }

    public async Task SetExpenses(int budgetID)
    {
        _selectedBudget.Expenses = await _unitOfWork.Expenses.GetExpensesByBudget(budgetID);
    }

    public void MarkExpenseForDeletion(Expense expense)
    {
        _expensesForDeletion.Add(expense);
    }

    public void AddNewBudget(Budget budget)
    {
        _budgets.Add(budget);
    }

    public void MarkBudgetForDeletion(Budget budget)
    {
        _budgetsForDeletion.Add(budget);
    }

    public async Task SaveChanges()
    {
        try
        {
            await SaveBudgets();
            await RemoveDeletedExpenses();
        }
        catch (Exception e)
        {
            Console.WriteLine($"There was an error saving changes from the budgeting service: {e.Message}");
            throw;
        }
    }

    public async Task SaveBudgets()
    {
        try
        {
            foreach (var budget in _budgets)
            {
                if (budget.IsNew)
                {
                    await _unitOfWork.Budgets.AddAsync(budget);
                    budget.IsNew = false;
                }
                else if (budget.IsUpdated)
                {
                    await _unitOfWork.Budgets.UpdateAsync(budget);
                    budget.IsUpdated = false;
                }
            }

            foreach (var budget in _budgetsForDeletion)
            {
                await _unitOfWork.Budgets.RemoveAsync(budget);
            }

            _budgetsForDeletion.Clear();
        }
        catch (Exception e)
        {
            Console.WriteLine($"There was an error saving budgets from the budgeting service: {e.Message}");
            throw;
        }
    }

    private async Task RemoveDeletedExpenses()
    {
        try
        {
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
