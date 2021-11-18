namespace CoinConstraint.Server.Controllers.Budgeting;

[Route("api/[controller]")]
[ApiController]
public class ExpensesController : ControllerBase
{
    private readonly IExpenseRepository _expenseRepository;

    public ExpensesController(IExpenseRepository expenseRepository)
    {
        _expenseRepository = expenseRepository;
    }

    [HttpGet]
    public async Task<ActionResult<List<Expense>>> GetExpensesAsync()
    {
        try
        {
            var expenses = await _expenseRepository.GetAllAsync();
            return Ok(expenses);
        }
        catch (Exception e)
        {
            Console.WriteLine($"There was an error retrieving expenses: {e.Message}");
            throw;
        }
    }

    [HttpGet("{budgetID}")]
    public async Task<ActionResult<List<Expense>>> GetExpensesByBudget(int budgetID)
    {
        try
        {
            var expenses = _expenseRepository.GetExpensesByBudget(budgetID);
            return Ok(expenses);
        }
        catch (Exception e)
        {
            Console.WriteLine($"There was an error retrieving expenses: {e.Message}");
            throw;
        }
    }

    [HttpPost]
    public async Task SaveNewExpenseAsync(Expense expense)
    {
        try
        {
            await _expenseRepository.AddAsync(expense);
            await _expenseRepository.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error saving new expense: {e.Message}");
            throw;
        }
    }

    [HttpPut]
    public async Task UpdateExistingExpense(Expense expense)
    {
        try
        {
            _expenseRepository.Update(expense);
            await _expenseRepository.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error updating existing expense: {e.Message}");
            throw;
        }
    }

    [HttpDelete]
    public async Task DeleteExpense(Expense expense)
    {
        try
        {
            _expenseRepository.Remove(expense);
            await _expenseRepository.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error deleting expense: {e.Message}");
            throw;
        }
    }

    [HttpDelete]
    public async Task DeleteExpenses(List<Expense> expenses)
    {
        try
        {
            _expenseRepository.RemoveRange(expenses);
            await _expenseRepository.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error deleting expenses: {e.Message}");
            throw;
        }
    }
}
