using CoinConstraint.Server.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace CoinConstraint.Server.Controllers.Budgeting;

[Route("api/[controller]")]
[ApiController]
public class NotesController : ControllerBase
{
    private readonly INoteRepository _noteRepository;
    private readonly IAuthorizationService _authorizationService;

    public NotesController(INoteRepository noteRepository, IAuthorizationService authorizationService)
    {
        _noteRepository = noteRepository;
        _authorizationService = authorizationService;
    }

    //[HttpGet]
    //public async Task<ActionResult<List<Note>>> GetNotesAsync()
    //{
    //    try
    //    {
    //        var notes = await _noteRepository.GetAllAsync();
    //        return Ok(notes);
    //    }
    //    catch (Exception e)
    //    {
    //        Console.WriteLine($"There was an error retrieving notes: {e.Message}");
    //        throw;
    //    }
    //}

    [HttpGet("{budgetID}")]
    public async Task<ActionResult<List<Expense>>> GetNotesByBudget(int budgetID)
    {
        try
        {
            var notes = _noteRepository.GetNotesByBudget(budgetID);

            foreach (var note in notes)
            {
                if ((await ActionIsAuthorized(note, Operations.Read)) == false)
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
            if ((await ActionIsAuthorized(note, Operations.Delete)) == false)
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
    public async Task<ActionResult> DeleteExpenses(List<Note> notes)
    {
        try
        {
            foreach (var note in notes)
            {
                if ((await ActionIsAuthorized(note, Operations.Delete)) == false)
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

    private async Task<bool> ActionIsAuthorized(Note note, OperationAuthorizationRequirement requirement)
    {
        var authorizationResult = await _authorizationService.AuthorizeAsync(User, note, requirement);
        if (!authorizationResult.Succeeded) return false;

        return true;
    }

}
