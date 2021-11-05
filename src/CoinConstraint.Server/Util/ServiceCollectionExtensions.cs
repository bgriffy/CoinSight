using CoinConstraint.Application.DataAccess;
using CoinConstraint.Domain.AggregateModels.BudgetAggregate.Repositories;
using CoinConstraint.Server.Infrastructure.Budgets;
using CoinConstraint.Server.Infrastructure.DataAccess;
using Microsoft.Extensions.DependencyInjection;

namespace CoinConstraint.Server.Util
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDataAccessServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<ICCUnitOfWork, CCUnitOfWork>();
        }

        public static void AddRepositores(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IBillRepository, BillRepository>();
            serviceCollection.AddScoped<IBudgetRepository, BudgetRepository>();
            serviceCollection.AddScoped<IExpenseRepository, ExpenseRepository>();
            serviceCollection.AddScoped<IReminderRepository, ReminderRepository>();
            serviceCollection.AddScoped<ITotalRepository, TotalRepository>();
            serviceCollection.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
