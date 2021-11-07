﻿using CoinConstraint.Client.Infrastructure.DataAccess;
using CoinConstraint.Domain.AggregateModels.BudgetAggregate;
using CoinConstraint.Domain.AggregateModels.BudgetAggregate.Repositories;
using System.Net.Http;

namespace CoinConstraint.Client.Infrastructure.Budgeting
{
    public class ClientTotalRepository : ClientsideRepository<Total>, ITotalRepository
    {
        private readonly HttpClient _httpClient;

        public ClientTotalRepository(HttpClient httpClient) : base(httpClient, "api/Totals")
        {
            _httpClient = httpClient;
        }
    }
}