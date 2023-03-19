namespace CoinConstraint.Server.Infrastructure.Budgeting;

public class NoteRepository : ServersideRepository<Note>, INoteRepository
{
    private readonly CoinConstraintContext _context;

    public NoteRepository(CoinConstraintContext context) : base(context)
    {
        _context = context;
    }

    public List<Note> GetNotesByBudget(int budgetID)
    {
        return _context.Notes.Where(e => e.BudgetID == budgetID).ToList();
    }

    public bool NoteExists(int? noteID, int? budgetID)
    {
        return _context.Notes.Any(n => n.NoteID == noteID && n.BudgetID == budgetID);
    }
}
