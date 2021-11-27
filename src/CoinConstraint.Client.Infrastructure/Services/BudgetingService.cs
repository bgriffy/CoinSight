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
            await LoadBudgets();
            _selectedBudget = _budgets.FirstOrDefault();
            _expensesForDeletion = new List<Expense>();
            if (_selectedBudget != null)
            {
                await SetExpenses(_selectedBudget.ID);
                await SetNotes(_selectedBudget.ID);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    private async Task LoadBudgets()
    {
        _budgets = (List<Budget>)await _unitOfWork.Budgets.GetAllAsync();
        _budgetsForDeletion = new List<Budget>();
    }

    public async Task SetSelectedBudget(Budget selectedBudget)
    {
        _selectedBudget = selectedBudget;
        await SetExpenses(_selectedBudget.ID);
        await SetNotes(_selectedBudget.ID);
    }

    public List<Budget> GetAllBudgets()
    {
        return _budgets;
    }

    public async Task SetExpenses(int budgetID)
    {
        _selectedBudget.Expenses = await _unitOfWork.Expenses.GetExpensesByBudget(budgetID);
    }

    private async Task SetNotes(int budgetID)
    {
        _selectedBudget.Notes = await _unitOfWork.Notes.GetNotesByBudgetID(budgetID);
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

    public async Task SaveChanges(bool removeDeletedExpenses = false)
    {
        try
        {
            foreach (var budget in _budgets)
            {
                if (budget.IsNew)
                {
                    await SaveNewBudget(budget);
                }
                else if (budget.IsUpdated)
                {
                    await SaveBudget(budget);
                }
            }

            await RemoveDeletedBudgets();
            
            if(removeDeletedExpenses)
            {
                await RemoveDeletedExpenses();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"There was an error saving budgets from the budgeting service: {e.Message}");
            throw;
        }
    }

    private async Task SaveBudget(Budget budget)
    {
        await _unitOfWork.Budgets.UpdateAsync(budget);

        budget.IsNew = false;
        budget.IsUpdated = false;

        foreach (var expense in budget.Expenses)
        {
            budget.IsUpdated = false;
            budget.IsNew = false; 
        }
    }

    private async Task SaveNewBudget(Budget budget)
    {
        var newID = await _unitOfWork.Budgets.AddBudget(budget);
        budget.ID = newID ?? 0;

        budget.IsNew = false;
        budget.IsUpdated = false;

        foreach (var expense in budget.Expenses)
        {
            expense.BudgetID = budget.ID;
            expense.IsUpdated = false;
            expense.IsNew = false;
        }
    }

    public async Task RemoveDeletedBudgets()
    {
        try
        {
            await _unitOfWork.Budgets.RemoveRangeAsync(_budgetsForDeletion);
            _budgetsForDeletion.Clear();
        }
        catch (Exception e)
        {
            Console.WriteLine($"There was an error removing budgets from the budgeting service: {e.Message}");
            throw;
        }
    }

    private async Task RemoveDeletedExpenses()
    {
        try
        {
            await _unitOfWork.Expenses.RemoveRangeAsync(_expensesForDeletion);
            _expensesForDeletion.Clear();
        }
        catch (Exception e)
        {
            Console.WriteLine($"There was an error removing expenses from the budgeting service: {e.Message}");
            throw;
        }
    }
    
    public Budget CloneBudget(Budget budget)
    {
        var newBudget = budget.Clone();
        return newBudget;
    }
}
