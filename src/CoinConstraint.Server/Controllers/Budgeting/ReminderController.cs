using CoinConstraint.Domain.AggregateModels.BudgetAggregate;
using CoinConstraint.Domain.AggregateModels.BudgetAggregate.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CoinConstraint.Server.Controllers.Budgeting;

[Route("api/[controller]")]
[ApiController]
public class ReminderController : ControllerBase
{
    private readonly IReminderRepository _totalsRepository;

    public ReminderController(IReminderRepository totalsRepository)
    {
        _totalsRepository = totalsRepository;
    }

    [HttpGet]
    public async Task<ActionResult<List<Reminder>>> GetRemindersAsync()
    {
        try
        {
            var reminders = await _totalsRepository.GetAllAsync();
            return Ok(reminders);
        }
        catch (System.Exception e)
        {
            Console.WriteLine($"There was an error retrieving reminders: {e.Message}");
            throw;
        }
    }

}
