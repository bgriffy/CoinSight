namespace CoinConstraint.Server.Controllers.Budgeting;

[Route("api/[controller]")]
[ApiController]
public class BudgetController : ControllerBase
{
    private readonly IBudgetRepository _budgetRepository;

    public BudgetController(IBudgetRepository budgetRepository)
    {
        _budgetRepository = budgetRepository;
    }

    [HttpGet]
    public async Task<ActionResult<List<Budget>>> GetBudgetsAsync()
    {
        try
        {
            var budgets = await _budgetRepository.GetAllAsync();
            return Ok(budgets);
        }
        catch (System.Exception e)
        {
            Console.WriteLine($"There was an error retrieving budgets: {e.Message}");
            throw;
        }
    }

    [HttpPost]
    public async Task SaveNewBudgetAsync(Budget budget)
    {
        try
        {
            await _budgetRepository.AddAsync(budget);
            await _budgetRepository.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error saving new budget: {e.Message}");
            throw;
        }
    }

    [HttpPut]
    public async Task UpdateBudget(Budget budget)
    {
        try
        {
            _budgetRepository.Update(budget);
            await _budgetRepository.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error updating existing budget: {e.Message}");
            throw;
        }
    }

    [HttpDelete]
    public async Task DeleteBudget(Budget budget)
    {
        try
        {
            _budgetRepository.Remove(budget);
            await _budgetRepository.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error deleting budget: {e.Message}");
            throw;
        }
    }

    [HttpDelete]
    public async Task DeleteBudgets(List<Budget> budgets)
    {
        try
        {
            _budgetRepository.RemoveRange(budgets);
            await _budgetRepository.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error deleting budgets: {e.Message}");
            throw;
        }
    }
}
