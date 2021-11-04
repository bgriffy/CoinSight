using CoinConstraint.Client.Infrastructure.DataAccess;
using CoinConstraint.Domain.AggregateModels.BudgetAggregate;
using CoinConstraint.Domain.AggregateModels.BudgetAggregate.Repositories;
using System.Net.Http;

namespace CoinConstraint.Client.Infrastructure.Budgeting
{
    public class ClientReminderRepository : ClientsideRepository<Reminder>, IReminderRepository
    {
        private readonly HttpClient _httpClient;

        public ClientReminderRepository(HttpClient httpClient) : base(httpClient, "api/Reminders")
        {
            _httpClient = httpClient;
        }
    }
}
