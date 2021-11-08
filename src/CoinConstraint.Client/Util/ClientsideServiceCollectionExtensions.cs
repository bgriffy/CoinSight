using CoinConstraint.Application.Budgeting;
using CoinConstraint.Application.DataAccess;
using CoinConstraint.Client.Infrastructure.Budgeting;
using CoinConstraint.Client.Infrastructure.DataAccess;
using CoinConstraint.Client.Infrastructure.Services;
using CoinConstraint.Domain.AggregateModels.BudgetAggregate.Repositories;
using CoinConstraint.Domain.AggregateModels.BudgetAggregate.Repositories.Client;
using CoinConstraint.Domain.AggregateModels.BudgetAggregate.Repositories.Clientside;
using Microsoft.Extensions.DependencyInjection;

namespace CoinConstraint.Client.Util
{
    public static class ClientsideServiceCollectionExtensions
    {
        public static void AddDataAccessServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IClientsideCCUnitOfWork, ClientsideCCUnitOfWork>();
        }

        public static void AddRepositores(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IClientsideBillRepository, ClientBillRepository>();
            serviceCollection.AddScoped<IClientsideExpenseRepository, ClientExpenseRepository>();
            serviceCollection.AddScoped<IClientsideBudgetRepository, ClientBudgetRepository>();
            serviceCollection.AddScoped<IClientsideReminderRepository, ClientReminderRepository>();
            serviceCollection.AddScoped<IClientsideTotalRepository, ClientTotalRepository>();
            serviceCollection.AddScoped<IClientsideTotalRepository, ClientTotalRepository>();
            serviceCollection.AddScoped<IClientsideUserRepository, ClientUserRepository>();
        }

        public static void AddApplicationServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IBudgetingService, BudgetingService>();
        }
    }
}
