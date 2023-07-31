using CoinSight.Application.Expenses.Queries.GetExpenseByBudgetId;
using MediatR;

namespace CoinConstraint.Server.Controllers.Budgeting;


[Route("api/[controller]")]
[ApiController]
[Authorize]

public class ExpensesController : ApiController
{
    private readonly IExpenseRepository _expenseRepository;
    private readonly ICCAuthorizationService _authorizationService;
    private readonly ISender _sender;

    public ExpensesController(IExpenseRepository expenseRepository, ICCAuthorizationService authorizationService, ISender sender)
        : base(sender)
    {
        _expenseRepository = expenseRepository;
        _authorizationService = authorizationService;
        _sender = sender;
    }

    [HttpGet("{budgetID}")]
    public async Task<IActionResult> GetExpensesByBudget(int budgetID)
    {
        try
        {
            var expenses = _expenseRepository.GetExpensesByBudget(budgetID);

            var command = new GetExpensesByBudgetId(budgetID);

            var result = await _sender.Send(command);

            if (!(await _authorizationService.AuthorizeExpenseView(User, expenses)))
            {
                return Unauthorized();
            }

            return result.IsSuccess ? Ok(expenses) : BadRequest(result.Error);
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

            await _expenseRepository.AddExpense(expense);

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

            await _expenseRepository.UpdateExpense(expense);

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
