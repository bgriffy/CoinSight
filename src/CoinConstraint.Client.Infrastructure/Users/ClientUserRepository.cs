using CoinConstraint.Domain.AggregateModels.UserAggregate.Entities;

namespace CoinConstraint.Client.Infrastructure.Users;

public class ClientUserRepository : ClientsideRepository<User>, IClientsideUserRepository
{
    private readonly HttpClient _httpClient;

    public ClientUserRepository(HttpClient httpClient) : base(httpClient, "api/Users")
    {
        _httpClient = httpClient;
    }
}
