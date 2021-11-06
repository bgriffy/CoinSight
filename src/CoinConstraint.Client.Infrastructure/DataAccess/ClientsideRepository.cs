using CoinConstraint.Domain.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace CoinConstraint.Client.Infrastructure.DataAccess
{
    public class ClientsideRepository<T> : IRepository<T> where T : class
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
            await _httpClient.PostAsJsonAsync<T>(_apiEndpoint, entity);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _httpClient.PostAsJsonAsync(_apiEndpoint, entities);
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            var results = (IQueryable<T>) await _httpClient.GetFromJsonAsync<List<T>>(_apiEndpoint);
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
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri("http://mydomain/api/something"),
                Content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json")
            };
            await _httpClient.SendAsync(request);
        }

        public async Task RemoveRangeAsync(IEnumerable<T> entities)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri("http://mydomain/api/something"),
                Content = new StringContent(JsonConvert.SerializeObject(entities), Encoding.UTF8, "application/json")
            };
            await _httpClient.SendAsync(request);
        }

        public async Task UpdateAsync(T entity)
        {
            await _httpClient.PutAsJsonAsync(_apiEndpoint, entity);
        }
    }
}
