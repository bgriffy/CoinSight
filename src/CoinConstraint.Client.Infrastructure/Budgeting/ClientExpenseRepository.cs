using CoinConstraint.Client.Infrastructure.DataAccess;
using CoinConstraint.Domain.AggregateModels.BudgetAggregate;
using CoinConstraint.Domain.AggregateModels.BudgetAggregate.Repositories.Clientside;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace CoinConstraint.Client.Infrastructure.Budgeting
{
    public class ClientExpenseRepository : ClientsideRepository<Expense>, IClientsideExpenseRepository
    {
        private readonly HttpClient _httpClient;
        private string _endpoint;

        public ClientExpenseRepository(HttpClient httpClient) : base(httpClient, "api/Expenses")
        {
            _httpClient = httpClient;
            _endpoint = "api/Expenses";
        }

        public async Task<List<Expense>> GetExpensesByBudget(int budgetID)
        {
            var expenses = await _httpClient.GetFromJsonAsync<List<Expense>>($"{_endpoint}/{budgetID}");
            return expenses;
        }
    }
}
