using CoinConstraint.Domain.AggregateModels.BudgetingAggregate.Entities;

namespace CoinConstraint.Client.Infrastructure.Users;

public class ClientReminderRepository : ClientsideRepository<Reminder>, IClientsideReminderRepository
{
    private readonly HttpClient _httpClient;

    public ClientReminderRepository(HttpClient httpClient) : base(httpClient, "api/Reminders")
    {
        _httpClient = httpClient;
    }
}
