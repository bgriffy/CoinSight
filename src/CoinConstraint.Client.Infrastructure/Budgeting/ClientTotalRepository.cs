using CoinConstraint.Client.Infrastructure.DataAccess;
using CoinConstraint.Domain.AggregateModels.BudgetAggregate;
using CoinConstraint.Domain.AggregateModels.BudgetAggregate.Repositories.Clientside;
using System.Net.Http;

namespace CoinConstraint.Client.Infrastructure.Budgeting
{
    public class ClientTotalRepository : ClientsideRepository<Total>, IClientsideTotalRepository
    {
        private readonly HttpClient _httpClient;

        public ClientTotalRepository(HttpClient httpClient) : base(httpClient, "api/Totals")
        {
            _httpClient = httpClient;
        }
    }
}
