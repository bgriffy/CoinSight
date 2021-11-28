using CoinConstraint.Domain.AggregateModels.BudgetingAggregate.Entities;

namespace CoinConstraint.Client.Infrastructure.Budgeting;

public class ClientNoteRepository : ClientsideRepository<Note>, IClientsideNoteRepository
{
    private readonly HttpClient _httpClient;
    private string _endPoint; 

    public ClientNoteRepository(HttpClient httpClient) : base(httpClient, "api/Notes")
    {
        _httpClient = httpClient;
        _endPoint = "api/Notes";
    }

    public async Task<List<Note>> GetNotesByBudgetID(int? budgetID)
    {
        var notes = await _httpClient.GetFromJsonAsync<List<Note>>($"{_endPoint}/{budgetID}");
        return notes;
    }

}
