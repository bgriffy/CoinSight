using CoinConstraint.Domain.AggregateModels.BudgetingAggregate.Entities;

namespace CoinConstraint.Client.Infrastructure.Users;

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
