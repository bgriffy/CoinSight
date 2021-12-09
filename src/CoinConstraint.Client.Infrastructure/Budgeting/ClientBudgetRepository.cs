using CoinConstraint.Domain.AggregateModels.BudgetingAggregate.Entities;

namespace CoinConstraint.Client.Infrastructure.Users;

public class ClientBudgetRepository : ClientsideRepository<Budget>, IClientsideBudgetRepository
{
    private readonly HttpClient _httpClient;
    private string _endpoint;

    public ClientBudgetRepository(HttpClient httpClient) : base(httpClient, "api/Budget")
    {
        _httpClient = httpClient;
        _endpoint = "api/Budget";
    }

    public async Task<int?> AddBudget(Budget budget)
    {
        try
        {
            var newRecordID = await base.AddAsyncAndReturnIdentity(budget);
            return Int32.Parse((String.IsNullOrWhiteSpace(newRecordID) ? "0" : newRecordID));
        }
        catch (Exception e)
        {
            Console.WriteLine($"There was an error adding a new budget from ClientBudgetRepository: {e.Message}");
            throw;
        }
    }

    public async Task<List<Budget>> GetBudgetsByUser(Guid userID)
    {
        var budgets = await _httpClient.GetFromJsonAsync<List<Budget>>($"{_endpoint}/{userID}");
        return budgets;
    }
}
