using CoinConstraint.Domain.AggregateModels.BudgetingAggregate.Entities;

namespace CoinConstraint.Client.Infrastructure.Budgeting;

public class ClientNoteRepository : ClientsideRepository<Note>, IClientsideNoteRepository
{
    private readonly HttpClient _httpClient;

    public ClientNoteRepository(HttpClient httpClient) : base(httpClient, "api/Notes")
    {
        _httpClient = httpClient;
    }
}
