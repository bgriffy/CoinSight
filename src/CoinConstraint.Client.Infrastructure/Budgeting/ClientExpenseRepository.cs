using CoinConstraint.Client.Infrastructure.DataAccess;
using CoinConstraint.Domain.AggregateModels.BudgetAggregate;
using CoinConstraint.Domain.AggregateModels.BudgetAggregate.Repositories;
using System.Net.Http;

namespace CoinConstraint.Client.Infrastructure.Budgeting
{
    public class ClientExpenseRepository : ClientsideRepository<Expense>, IExpenseRepository
    {
        private readonly HttpClient _httpClient;

        public ClientExpenseRepository(HttpClient httpClient) : base(httpClient, "api/Expenses")
        {
            _httpClient = httpClient;
        }
    }
}
