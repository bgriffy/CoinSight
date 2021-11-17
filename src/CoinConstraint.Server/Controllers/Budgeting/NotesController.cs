namespace CoinConstraint.Server.Controllers.Budgeting;

[Route("api/[controller]")]
[ApiController]
public class NotesController : ControllerBase
{
    private readonly INoteRepository _noteRepository;

    public NotesController(INoteRepository noteRepository)
    {
        _noteRepository = noteRepository;
    }

    [HttpGet]
    public async Task<ActionResult<List<Note>>> GetNotesAsync()
    {
        try
        {
            var notes = await _noteRepository.GetAllAsync();
            return Ok(notes);
        }
        catch (Exception e)
        {
            Console.WriteLine($"There was an error retrieving notes: {e.Message}");
            throw;
        }
    }

    [HttpGet("{budgetID}")]
    public async Task<ActionResult<List<Expense>>> GetNotesByBudget(int budgetID)
    {
        try
        {
            var notes = _noteRepository.GetNotesByBudget(budgetID);
            return Ok(notes);
        }
        catch (Exception e)
        {
            Console.WriteLine($"There was an error retrieving notes: {e.Message}");
            throw;
        }
    }

}
