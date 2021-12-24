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

    [HttpGet("{userID}")]
    public async Task<ActionResult<List<Budget>>> GetBudgetsByUser(Guid userID)
    {
        try
        {
            if (UserUnauthorized(userID)) return Unauthorized();

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
            if (UserUnauthorized(budget.UUID)) return Unauthorized();

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
    public async Task<ActionResult> UpdateBudget(Budget budget)
    {
        try
        {
            if (UserUnauthorized(budget.UUID)) return Unauthorized();

            _budgetRepository.Update(budget);
            await _budgetRepository.SaveChangesAsync();

            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error updating existing budget: {e.Message}");
            throw;
        }
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteBudget(Budget budget)
    {
        try
        {
            if (UserUnauthorized(budget.UUID)) return Unauthorized();

            _budgetRepository.Remove(budget);
            await _budgetRepository.SaveChangesAsync();

            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error deleting budget: {e.Message}");
            throw;
        }
    }

    [HttpDelete("DeleteMultiple")]
    public async Task<ActionResult> DeleteBudgets(List<Budget> budgets)
    {
        try
        {
            foreach (var budget in budgets)
            {
                if (UserUnauthorized(budget.UUID)) return Unauthorized();
            }

            _budgetRepository.RemoveRange(budgets);
            await _budgetRepository.SaveChangesAsync();

            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error deleting budgets: {e.Message}");
            throw;
        }
    }

    // TODO: Should probably move this to a service. 
    private bool UserUnauthorized(Guid userID)
    {
        var userIdFromClaim = User.GetUserId();

        if (userID.ToString() != userIdFromClaim) return true;

        return false; 
    }
}
