namespace CoinConstraint.Client.Infrastructure.DataAccess;

public class ClientsideRepository<T> : IClientsideRepository<T> where T : class
{
    private readonly HttpClient _httpClient;
    private readonly string _apiEndpoint;

    public ClientsideRepository(HttpClient httpClient, string apiEndpoint)
    {
        _httpClient = httpClient;
        _apiEndpoint = apiEndpoint;
    }

    public async Task AddAsync(T entity)
    {
        var json = JsonConvert.SerializeObject(entity);
        await _httpClient.PostAsync(_apiEndpoint, new StringContent(json, Encoding.UTF8, "application/json"));
    }

    public async Task<string?> AddAsyncAndReturnIdentity(T entity)
    {
        var json = JsonConvert.SerializeObject(entity);
        var response = await _httpClient.PostAsync(_apiEndpoint, new StringContent(json, Encoding.UTF8, "application/json"));
        if (response == null)
        {
            Console.WriteLine("Error: POST API request from Clientside repository did not return a response.");
            return null;
        }
        var newRecordID = await response.Content.ReadAsStringAsync();
        return newRecordID;
    }


    public async Task AddRangeAsync(IEnumerable<T> entities)
    {
        var json = JsonConvert.SerializeObject(entities);
        await _httpClient.PostAsync(_apiEndpoint, new StringContent(json, Encoding.UTF8, "application/json"));
    }

    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
    {
        var results = (IQueryable<T>)await _httpClient.GetFromJsonAsync<List<T>>(_apiEndpoint);
        return results.Where(predicate);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<T>>(_apiEndpoint);
    }

    public async Task RemoveAllAsync()
    {
        await _httpClient.DeleteAsync(_apiEndpoint);
    }

    public async Task RemoveAsync(T entity)
    {
        var request = new HttpRequestMessage(HttpMethod.Delete, _apiEndpoint);
        request.Content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
        await _httpClient.SendAsync(request);
    }

    public async Task RemoveRangeAsync(IEnumerable<T> entities)
    {
        var request = new HttpRequestMessage(HttpMethod.Delete, $"{_apiEndpoint}/DeleteMultiple");
        request.Content = new StringContent(JsonConvert.SerializeObject(entities), Encoding.UTF8, "application/json");
        await _httpClient.SendAsync(request);
    }

    public Task SaveChangesAsync()
    {
        //TODO: Think I will need to implement this once the offline storage is setup. 
        throw new NotImplementedException();
    }

    public async Task UpdateAsync(T entity)
    {
        await _httpClient.PutAsJsonAsync(_apiEndpoint, entity);
    }
}
