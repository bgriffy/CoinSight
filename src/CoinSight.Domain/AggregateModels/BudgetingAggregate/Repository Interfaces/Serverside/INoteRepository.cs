namespace CoinConstraint.Domain.AggregateModels.BudgetingAggregate.RepositoryInterfaces.Serverside;

public interface INoteRepository : IServersideRepository<Note>
{
    List<Note> GetNotesByBudget(int budgetID);
    bool NoteExists(int? noteID, int? budgetID);
}
