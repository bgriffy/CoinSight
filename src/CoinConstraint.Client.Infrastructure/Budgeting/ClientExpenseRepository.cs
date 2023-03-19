using CoinConstraint.Domain.AggregateModels.BudgetingAggregate.Entities;
using System.Net.Http.Json;

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

    public async Task<List<Expense>> GetExpensesByBudget(int? budgetID)
    {
        var expenses = await _httpClient.GetFromJsonAsync<List<Expense>>($"{_endpoint}/{budgetID}");
        return expenses;
    }

    public async Task UpdateExpense(Expense expense)
    {
        try
        {
            await _httpClient.PutAsJsonAsync(_endpoint, expense);
        }
        catch (Exception e) 
        {
            Console.WriteLine($"There was an error updating expense: {e.Message}");
            throw;
        }
    }

    public async Task AddExpense(Expense expense)
    {
        try
        {
            await _httpClient.PostAsJsonAsync(_endpoint, expense);
        }
        catch (Exception e)
        {
            Console.WriteLine($"There was an error adding expense: {e.Message}");
            throw;
        }
    }
}
