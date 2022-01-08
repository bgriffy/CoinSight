using CoinConstraint.Application.Identity;
using CoinConstraint.Server.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace CoinConstraint.Server.Controllers.Budgeting;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class BudgetController : ControllerBase
{
    private readonly ICCUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUserService;
    private readonly IAuthorizationService _authorizationService;

    public BudgetController(ICCUnitOfWork unitOfWork, 
                            ICurrentUserService currentUserService, 
                            IAuthorizationService authorizationService)
    {
        _unitOfWork = unitOfWork;
        _currentUserService = currentUserService;
        _authorizationService = authorizationService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Budget>>> GetCurrentUserBudgets()
    {
        try
        {
            var userID = await _currentUserService.GetCurrentUserID();
            var budgets = await _unitOfWork.Budgets.GetBudgetsByUser(userID);

            foreach (var budget in budgets)
            {
                if ((await ActionIsAuthorized(budget, Operations.Read)) == false)
                {
                    return Unauthorized();
                }
            }

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
            if ((await ActionIsAuthorized(budget, Operations.Create)) == false)
            {
                return Unauthorized();
            }

            await _unitOfWork.Budgets.AddAsync(budget);
            await _unitOfWork.Budgets.SaveChangesAsync();

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
            if ((await ActionIsAuthorized(budget, Operations.Update)) == false)
            {
                return Unauthorized();
            }

            _unitOfWork.Budgets.Update(budget);
            await _unitOfWork.Budgets.SaveChangesAsync();

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
            if ((await ActionIsAuthorized(budget, Operations.Delete)) == false)
            {
                return Unauthorized();
            }

            if((budget.Expenses?.Count?? 0) > 0)
            {
                _unitOfWork.Expenses.RemoveRange(budget.Expenses);
            }

            if ((budget.Notes?.Count ?? 0) > 0)
            {
                _unitOfWork.Notes.RemoveRange(budget.Notes);
            }

            _unitOfWork.Budgets.Remove(budget);

            await _unitOfWork.Budgets.SaveChangesAsync();

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
                if ((await ActionIsAuthorized(budget, Operations.Delete)) == false)
                {
                    return Unauthorized();
                }

                if ((budget.Expenses?.Count ?? 0) > 0)
                {
                    _unitOfWork.Expenses.RemoveRange(budget.Expenses);
                }

                if ((budget.Notes?.Count ?? 0) > 0)
                {
                    _unitOfWork.Notes.RemoveRange(budget.Notes);
                }
            }

            _unitOfWork.Budgets.RemoveRange(budgets);
            await _unitOfWork.Budgets.SaveChangesAsync();

            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error deleting budgets: {e.Message}");
            throw;
        }
    }

    private async Task<bool> ActionIsAuthorized(Budget budget, OperationAuthorizationRequirement requirement)
    {
        var authorizationResult = await _authorizationService.AuthorizeAsync(User, budget, requirement);
        if (!authorizationResult.Succeeded) return false;

        return true;
    }
}
