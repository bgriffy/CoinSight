namespace CoinConstraint.Server.Controllers.Budgeting;

[Route("api/[controller]")]
[ApiController]
[Authorize]

public class ReminderController : ControllerBase
{
    private readonly IReminderRepository _remindersRepository;

    public ReminderController(IReminderRepository remindersRepository)
    {
        _remindersRepository = remindersRepository;
    }

    //[HttpGet]
    //public async Task<ActionResult<List<Reminder>>> GetRemindersAsync()
    //{
    //    try
    //    {
    //        var reminders = await _totalsRepository.GetAllAsync();
    //        return Ok(reminders);
    //    }
    //    catch (System.Exception e)
    //    {
    //        Console.WriteLine($"There was an error retrieving reminders: {e.Message}");
    //        throw;
    //    }
    //}
}
