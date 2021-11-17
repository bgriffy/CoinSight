namespace CoinConstraint.Client.Util;

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
        serviceCollection.AddScoped<IClientsideNoteRepository, ClientNoteRepository>();
    }

    public static void AddApplicationServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IBudgetingService, BudgetingService>();
    }
}
