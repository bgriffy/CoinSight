using Blazored.LocalStorage;
using BlazorPro.BlazorSize;
using CoinConstraint.Application.Budgeting;
using CoinConstraint.Application.DataAccess;
using CoinConstraint.Application.Identity;
using CoinConstraint.Application.Transactions;
using CoinConstraint.Client.Infrastructure.Authentication;
using CoinConstraint.Client.Infrastructure.Budgeting;
using CoinConstraint.Client.Infrastructure.DataAccess;
using CoinConstraint.Client.Infrastructure.Identity;
using CoinConstraint.Client.Infrastructure.Services;
using CoinConstraint.Client.Infrastructure.Users;
using CoinConstraint.Domain.AggregateModels.BudgetAggregate.Repositories.Clientside;
using CoinConstraint.Domain.AggregateModels.BudgetingAggregate.Repository_Interfaces.Clientside;
using CoinConstraint.Domain.AggregateModels.BudgetingAggregate.RepositoryInterfaces.Clientside;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace CoinSight.Client.Util;

public static class ClientsideServiceCollectionExtensions
{
    public static void AddDataAccessServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IClientsideCCUnitOfWork, ClientsideCCUnitOfWork>();
    }

    public static void AddRepositores(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IClientsideExpenseRepository, ClientExpenseRepository>();
        serviceCollection.AddScoped<IClientsideBudgetRepository, ClientBudgetRepository>();
        serviceCollection.AddScoped<IClientsideReminderRepository, ClientReminderRepository>();
        serviceCollection.AddScoped<IClientsideNoteRepository, ClientNoteRepository>();
        serviceCollection.AddScoped<IClientsideBudgetScheduleRepository, ClientBudgetScheduleRepository>();
    }

    public static void AddApplicationServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IBudgetingService, BudgetingService>();
        serviceCollection.AddScoped<ITransactionService, TransactionService>();
        serviceCollection.AddScoped<ICurrentUserService, ClientsideUserService>();
    }

    public static void AddThirdPartyServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddBlazoredLocalStorage();
        serviceCollection.AddMediaQueryService();
    }

    public static void AddAuthServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddAuthorizationCore();
        serviceCollection.AddScoped<AuthenticationStateProvider, CCAuthenticationStateProvider>();
        serviceCollection.AddScoped<ICCAuthenticationService, CCAuthenticationService>();
    }

}
