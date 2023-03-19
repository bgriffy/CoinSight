using CoinConstraint.Domain.AggregateModels.BudgetingAggregate.Entities;

namespace CoinConstraint.Client.Infrastructure.Users;

public class ClientTotalRepository : ClientsideRepository<Total>, IClientsideTotalRepository
{
    private readonly HttpClient _httpClient;

    public ClientTotalRepository(HttpClient httpClient) : base(httpClient, "api/Totals")
    {
        _httpClient = httpClient;
    }
}
