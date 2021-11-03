using CoinConstraint.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net.Http;
using System.Net.Http.Json;
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
            await _httpClient.PostAsJsonAsync(_apiEndpoint, entity);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _httpClient.PostAsJsonAsync(_apiEndpoint, entities);
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task RemoveAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task RemoveAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveRangeAsync(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
