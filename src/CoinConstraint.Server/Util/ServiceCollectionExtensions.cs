namespace CoinConstraint.Server.Util;

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
        serviceCollection.AddScoped<INoteRepository, NoteRepository>();
    }
}
