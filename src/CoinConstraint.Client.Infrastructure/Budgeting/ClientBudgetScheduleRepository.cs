using CoinConstraint.Domain.AggregateModels.BudgetingAggregate.Entities;
using CoinConstraint.Domain.AggregateModels.BudgetingAggregate.Repository_Interfaces.Clientside;

namespace CoinConstraint.Client.Infrastructure.Budgeting
{
    public class ClientBudgetScheduleRepository : ClientsideRepository<BudgetSchedule>, IClientsideBudgetScheduleRepository
    {
        private readonly HttpClient _httpClient;

        public ClientBudgetScheduleRepository(HttpClient httpClient) : base(httpClient, "api/BudgetSchedules")
        {
            _httpClient = httpClient;
        }
    }
}
