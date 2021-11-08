using CoinConstraint.Client.Infrastructure.DataAccess;
using CoinConstraint.Domain.AggregateModels.BudgetAggregate;
using CoinConstraint.Domain.AggregateModels.BudgetAggregate.Repositories.Clientside;
using System.Net.Http;
using System.Threading.Tasks;

namespace CoinConstraint.Client.Infrastructure.Budgeting
{
    public class ClientExpenseRepository : ClientsideRepository<Expense>, IClientsideExpenseRepository
    {
        private readonly HttpClient _httpClient;

        public ClientExpenseRepository(HttpClient httpClient) : base(httpClient, "api/Expenses")
        {
            _httpClient = httpClient;
        }

        public Task SaveChangesAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}
