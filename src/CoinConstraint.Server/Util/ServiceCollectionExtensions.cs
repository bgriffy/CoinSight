using CoinConstraint.Domain.AggregateModels.BudgetingAggregate.Repository_Interfaces.Serverside;

namespace CoinConstraint.Server.Util;

public static class ServiceCollectionExtensions
{
    public static void AddDataAccessServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<ICCUnitOfWork, CCUnitOfWork>();
    }

    public static void AddRepositores(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IBudgetRepository, BudgetRepository>();
        serviceCollection.AddScoped<IExpenseRepository, ExpenseRepository>();
        serviceCollection.AddScoped<IReminderRepository, ReminderRepository>();
        serviceCollection.AddScoped<INoteRepository, NoteRepository>();
        serviceCollection.AddScoped<IBudgetScheduleRepository, BudgetScheduleRepository>();
    }
}
