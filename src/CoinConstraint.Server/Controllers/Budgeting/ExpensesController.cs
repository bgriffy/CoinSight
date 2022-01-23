using CoinConstraint.Application.Identity;
using CoinConstraint.Server.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace CoinConstraint.Server.Controllers.Budgeting;


[Route("api/[controller]")]
[ApiController]
[Authorize]

public class ExpensesController : ControllerBase
{
    private readonly IExpenseRepository _expenseRepository;
    private readonly ICCAuthorizationService _authorizationService;

    public ExpensesController(IExpenseRepository expenseRepository, ICCAuthorizationService authorizationService)
    {
        _expenseRepository = expenseRepository;
        _authorizationService = authorizationService;
    }

    [HttpGet("{budgetID}")]
    public async Task<ActionResult<List<Expense>>> GetExpensesByBudget(int budgetID)
    {
        try
        {
            var userID = User.GetUserId();
            var expenses = _expenseRepository.GetExpensesByBudget(budgetID);

            foreach (var expense in expenses)
            {
                var actionIsAuthorized = await _authorizationService.ActionIsAuthorized(User, expense, Operations.Read);
                if (!actionIsAuthorized)
                {
                    return Unauthorized();
                }
            }

            return Ok(expenses);
        }
        catch (Exception e)
        {
            Console.WriteLine($"There was an error retrieving expenses: {e.Message}");
            throw;
        }
    }

    [HttpPost]
    public async Task<ActionResult> SaveNewExpenseAsync(Expense expense)
    {
        try
        {
            var actionIsAuthorized = await _authorizationService.ActionIsAuthorized(User, expense, Operations.Create);
            if (!actionIsAuthorized)
            {
                return Unauthorized();
            }

            await _expenseRepository.AddAsync(expense);
            await _expenseRepository.SaveChangesAsync();

            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error saving new expense: {e.Message}");
            throw;
        }
    }

    [HttpPut]
    public async Task<ActionResult> UpdateExistingExpense(Expense expense)
    {
        try
        {
            var actionIsAuthorized = await _authorizationService.ActionIsAuthorized(User, expense, Operations.Update);
            if (!actionIsAuthorized)
            {
                return Unauthorized();
            }

            _expenseRepository.Update(expense);
            await _expenseRepository.SaveChangesAsync();

            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error updating existing expense: {e.Message}");
            throw;
        }
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteExpense(Expense expense)
    {
        try
        {
            var actionIsAuthorized = await _authorizationService.ActionIsAuthorized(User, expense, Operations.Delete);
            if (!actionIsAuthorized)
            {
                return Unauthorized();
            }

            _expenseRepository.Remove(expense);
            await _expenseRepository.SaveChangesAsync();

            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error deleting expense: {e.Message}");
            throw;
        }
    }

    [HttpDelete("DeleteMultiple")]
    public async Task<ActionResult> DeleteExpenses(List<Expense> expenses)
    {
        try
        {
            foreach (var expense in expenses)
            {
                var actionIsAuthorized = await _authorizationService.ActionIsAuthorized(User, expense, Operations.Delete);
                if (!actionIsAuthorized)
                {
                    return Unauthorized();
                }
            }

            _expenseRepository.RemoveRange(expenses);
            await _expenseRepository.SaveChangesAsync();

            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error deleting expenses: {e.Message}");
            throw;
        }
    }
}
