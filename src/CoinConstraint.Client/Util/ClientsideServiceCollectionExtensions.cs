using CoinConstraint.Application.Budgeting;
using CoinConstraint.Application.DataAccess;
using CoinConstraint.Client.Infrastructure.Budgeting;
using CoinConstraint.Client.Infrastructure.DataAccess;
using CoinConstraint.Client.Infrastructure.Services;
using CoinConstraint.Domain.AggregateModels.BudgetAggregate.Repositories;
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
            serviceCollection.AddScoped<IBillRepository, ClientBillRepository>();
            serviceCollection.AddScoped<IBudgetRepository, ClientBudgetRepository>();
            serviceCollection.AddScoped<IReminderRepository, ClientReminderRepository>();
            serviceCollection.AddScoped<ITotalRepository, ClientTotalRepository>();
            serviceCollection.AddScoped<ITotalRepository, ClientTotalRepository>();
            serviceCollection.AddScoped<IUserRepository, ClientUserRepository>();
        }

        public static void AddApplicationServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IBudgetingService, BudgetingService>();
        }
    }
}
