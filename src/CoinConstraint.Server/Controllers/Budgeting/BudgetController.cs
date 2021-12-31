using CoinConstraint.Application.Identity;

namespace CoinConstraint.Server.Controllers.Budgeting;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class BudgetController : ControllerBase
{
    private readonly IBudgetRepository _budgetRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IAuthorizationService _authorizationService;

    public BudgetController(IBudgetRepository budgetRepository, 
                            ICurrentUserService currentUserService, 
                            IAuthorizationService authorizationService)
    {
        _budgetRepository = budgetRepository;
        _currentUserService = currentUserService;
        _authorizationService = authorizationService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Budget>>> GetCurrentUserBudgets()
    {
        try
        {
            var userID = await _currentUserService.GetCurrentUserID();
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
            if (await UserDoesNotOwnBudget(budget)) return Unauthorized();
            
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
            if (await UserDoesNotOwnBudget(budget)) return Unauthorized();

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
            if (await UserDoesNotOwnBudget(budget)) return Unauthorized();

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
                if (await UserDoesNotOwnBudget(budget)) return Unauthorized();
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

    private async Task<bool> UserDoesNotOwnBudget(Budget budgetResource)
    {
        try
        {
            //Grab budget from DB in case passed-in budget as been modified
            var budget = await _budgetRepository.FirstOrDefault(b => b.ID == budgetResource.ID); 
            if (budget == null && !budgetResource.IsNew) return true;

            var authorizationResult = await _authorizationService.AuthorizeAsync(User, budget ?? budgetResource, "BudgetAuthorPolicy");

            if (!authorizationResult.Succeeded) return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }

        return false;
    }
}
