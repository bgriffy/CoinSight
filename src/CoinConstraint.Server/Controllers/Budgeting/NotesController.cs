using CoinConstraint.Application.Identity;
using CoinConstraint.Server.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace CoinConstraint.Server.Controllers.Budgeting;

[Route("api/[controller]")]
[ApiController]
public class NotesController : ControllerBase
{
    private readonly INoteRepository _noteRepository;
    private readonly ICCAuthorizationService _authorizationService;

    public NotesController(INoteRepository noteRepository, ICCAuthorizationService authorizationService)
    {
        _noteRepository = noteRepository;
        _authorizationService = authorizationService;
    }

    [HttpGet("{budgetID}")]
    public async Task<ActionResult<List<Expense>>> GetNotesByBudget(int budgetID)
    {
        try
        {
            var notes = _noteRepository.GetNotesByBudget(budgetID);

            foreach (var note in notes)
            {
                var actionIsAuthorized = await _authorizationService.ActionIsAuthorized(User, note, Operations.Read);
                if (!actionIsAuthorized)
                {
                    return Unauthorized();
                }
            }

            return Ok(notes);
        }
        catch (Exception e)
        {
            Console.WriteLine($"There was an error retrieving notes: {e.Message}");
            throw;
        }
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteNote(Note note)
    {
        try
        {
            var actionIsAuthorized = await _authorizationService.ActionIsAuthorized(User, note, Operations.Delete);
            if (!actionIsAuthorized)
            {
                return Unauthorized();
            }

            _noteRepository.Remove(note);
            await _noteRepository.SaveChangesAsync();

            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error deleting note: {e.Message}");
            throw;
        }
    }

    [HttpDelete("DeleteMultiple")]
    public async Task<ActionResult> DeleteNotes(List<Note> notes)
    {
        try
        {
            foreach (var note in notes)
            {
                var actionIsAuthorized = await _authorizationService.ActionIsAuthorized(User, note, Operations.Delete);
                if (!actionIsAuthorized)
                {
                    return Unauthorized();
                }
            }

            _noteRepository.RemoveRange(notes);
            await _noteRepository.SaveChangesAsync();

            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error deleting notes: {e.Message}");
            throw;
        }
    }
}
