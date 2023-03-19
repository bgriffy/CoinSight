using CoinConstraint.Domain.AggregateModels.BudgetingAggregate.Entities;

namespace CoinConstraint.Client.Infrastructure.Users;

public class ClientBillRepository : ClientsideRepository<Bill>, IClientsideBillRepository
{
    private readonly HttpClient _httpClient;

    public ClientBillRepository(HttpClient httpClient) : base(httpClient, "api/Bills")
    {
        _httpClient = httpClient;
    }
}
