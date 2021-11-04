using CoinConstraint.Client.Infrastructure.DataAccess;
using CoinConstraint.Domain.AggregateModels.BudgetAggregate;
using CoinConstraint.Domain.AggregateModels.BudgetAggregate.Repositories;
using System.Net.Http;

namespace CoinConstraint.Client.Infrastructure.Budgeting
{
    public class ClientBudgetRepository : ClientsideRepository<Budget>, IBudgetRepository
    {
        private readonly HttpClient _httpClient;

        public ClientBudgetRepository(HttpClient httpClient) : base(httpClient, "api/Budgets")
        {
            _httpClient = httpClient;
        }
    }
}
