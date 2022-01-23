using CoinConstraint.Application.Identity;
using CoinConstraint.Server.Infrastructure.Identity;

namespace CoinConstraint.Server.Controllers.Budgeting;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class BudgetController : ControllerBase
{
    private readonly ICCUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUserService;
    private readonly ICCAuthorizationService _authorizationService;

    public BudgetController(ICCUnitOfWork unitOfWork,
                            ICurrentUserService currentUserService,
                            ICCAuthorizationService authorizationService)
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
                var actionIsAuthorized = await _authorizationService.ActionIsAuthorized(User, budget, Operations.Read);
                if (!actionIsAuthorized)
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
            var actionIsAuthorized = await _authorizationService.ActionIsAuthorized(User, budget, Operations.Create);
            if (!actionIsAuthorized)
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
            var actionIsAuthorized = await _authorizationService.ActionIsAuthorized(User, budget, Operations.Update);
            if (!actionIsAuthorized)
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
            var actionIsAuthorized = await _authorizationService.ActionIsAuthorized(User, budget, Operations.Delete);
            if (!actionIsAuthorized)
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
                var actionIsAuthorized = await _authorizationService.ActionIsAuthorized(User, budget, Operations.Delete);
                if (!actionIsAuthorized)
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
}
