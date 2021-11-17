using CoinConstraint.Domain.AggregateModels.BudgetingAggregate.Entities;

namespace CoinConstraint.Client.Infrastructure.Users;

public class ClientBudgetRepository : ClientsideRepository<Budget>, IClientsideBudgetRepository
{
    private readonly HttpClient _httpClient;

    public ClientBudgetRepository(HttpClient httpClient) : base(httpClient, "api/Budget")
    {
        _httpClient = httpClient;
    }
}
