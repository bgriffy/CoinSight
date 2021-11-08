using CoinConstraint.Client.Infrastructure.DataAccess;
using CoinConstraint.Domain.AggregateModels.BudgetAggregate;
using CoinConstraint.Domain.AggregateModels.BudgetAggregate.Repositories.Clientside;
using System.Net.Http;

namespace CoinConstraint.Client.Infrastructure.Budgeting
{
    public class ClientBillRepository : ClientsideRepository<Bill>, IClientsideBillRepository
    {
        private readonly HttpClient _httpClient;

        public ClientBillRepository(HttpClient httpClient) : base(httpClient, "api/Bills")
        {
            _httpClient = httpClient;
        }
    }
}
