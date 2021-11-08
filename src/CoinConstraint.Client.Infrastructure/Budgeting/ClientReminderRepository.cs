using CoinConstraint.Client.Infrastructure.DataAccess;
using CoinConstraint.Domain.AggregateModels.BudgetAggregate;
using CoinConstraint.Domain.AggregateModels.BudgetAggregate.Repositories.Clientside;
using System.Net.Http;

namespace CoinConstraint.Client.Infrastructure.Budgeting
{
    public class ClientReminderRepository : ClientsideRepository<Reminder>, IClientsideReminderRepository
    {
        private readonly HttpClient _httpClient;

        public ClientReminderRepository(HttpClient httpClient) : base(httpClient, "api/Reminders")
        {
            _httpClient = httpClient;
        }
    }
}
