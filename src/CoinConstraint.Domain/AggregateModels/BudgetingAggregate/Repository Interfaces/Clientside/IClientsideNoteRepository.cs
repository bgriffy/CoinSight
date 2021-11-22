namespace CoinConstraint.Domain.AggregateModels.BudgetingAggregate.RepositoryInterfaces.Clientside;

public interface IClientsideNoteRepository : IClientsideRepository<Note>
{
    Task<List<Note>> GetNotesByBudgetID(int budgetID);
}
