using CoinConstraint.Client.Infrastructure.DataAccess;
using CoinConstraint.Domain.AggregateModels.BudgetAggregate.Repositories;
using CoinConstraint.Domain.AggregateModels.BudgetAggregate.Repositories.Client;
using CoinConstraint.Domain.AggregateModels.UserAggregate;
using System.Net.Http;

namespace CoinConstraint.Client.Infrastructure.Budgeting
{
    public class ClientUserRepository : ClientsideRepository<User>, IClientsideUserRepository
    {
        private readonly HttpClient _httpClient;

        public ClientUserRepository(HttpClient httpClient) : base(httpClient, "api/Users")
        {
            _httpClient = httpClient;
        }
    }
}
