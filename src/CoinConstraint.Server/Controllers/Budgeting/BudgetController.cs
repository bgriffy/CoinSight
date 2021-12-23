using Microsoft.AspNetCore.Identity;

namespace CoinConstraint.Server.Controllers.Budgeting;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class BudgetController : ControllerBase
{
    private readonly IBudgetRepository _budgetRepository;

    public BudgetController(IBudgetRepository budgetRepository, UserManager<IdentityUser> userManager)
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

    [HttpGet("{userID}")]
    public async Task<ActionResult<List<Budget>>> GetBudgetsByUser(Guid userID)
    {
        try
        {
            var userIdFromClaim = User.GetUserId();

            if(userID.ToString() != userIdFromClaim) return Unauthorized();

            var budgets = await _budgetRepository.GetBudgetsByUser(userID);
            return Ok(budgets);
        }
        catch (System.Exception e)
        {
            Console.WriteLine($"There was an error retrieving budgets by user: {e.Message}");
            throw;
        }
    }

    [HttpPost]
    public async Task<ActionResult<int>> SaveNewBudgetAsync(Budget budget)
    {
        try
        {
            await _budgetRepository.AddAsync(budget);
            await _budgetRepository.SaveChangesAsync();
            return Ok(budget.ID);
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

    [HttpDelete("DeleteMultiple")]
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
