using CoinConstraint.Client.Infrastructure.Budgeting;
using CoinConstraint.Domain.AggregateModels.BudgetAggregate.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CoinConstraint.Client.Util
{
    public static class ClientsideServiceCollectionExtensions
    {
        public static void AddRepositores(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IBillRepository, ClientBillRepository>();
            serviceCollection.AddScoped<IBudgetRepository, ClientBudgetRepository>();
            serviceCollection.AddScoped<IReminderRepository, ClientReminderRepository>();
            serviceCollection.AddScoped<ITotalRepository, ClientTotalRepository>();
            serviceCollection.AddScoped<ITotalRepository, ClientTotalRepository>();
            serviceCollection.AddScoped<IUserRepository, ClientUserRepository>();
        }
    }
}
