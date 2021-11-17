namespace CoinConstraint.Server.Controllers.Budgeting;

[Route("api/[controller]")]
[ApiController]
public class TotalsController : ControllerBase
{
    private readonly ITotalRepository _totalsRepository;

    public TotalsController(ITotalRepository totalsRepository)
    {
        _totalsRepository = totalsRepository;
    }

    [HttpGet]
    public async Task<ActionResult<List<Total>>> GetTotalsAsync()
    {
        try
        {
            var totals = await _totalsRepository.GetAllAsync();
            return Ok(totals);
        }
        catch (System.Exception e)
        {
            Console.WriteLine($"There was an error retrieving totals: {e.Message}");
            throw;
        }
    }
}
