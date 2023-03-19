using Blazor.ModalDialog;
using Blazored.LocalStorage;
using CoinConstraint.Application.Identity;
using CoinConstraint.Application.Transactions;
using CoinConstraint.Client.Infrastructure.Authentication;
using CoinConstraint.Domain.AggregateModels.BudgetingAggregate.Repository_Interfaces.Clientside;
using Microsoft.AspNetCore.Components.Authorization;

namespace CoinConstraint.Client.Util;

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
    }

    public static void AddThirdPartyServices(this IServiceCollection serviceCollection)
    {
        serviceCollection
          .AddBlazorise(options =>
          {
              options.ChangeTextOnKeyPress = true;
          })
          .AddBootstrapProviders()
          .AddFontAwesomeIcons();
        serviceCollection.AddBlazoredLocalStorage();
        serviceCollection.AddMediaQueryService();
        serviceCollection.AddModalDialog();
    }

    public static void AddAuthServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddAuthorizationCore();
        serviceCollection.AddScoped<AuthenticationStateProvider, CCAuthenticationStateProvider>();
        serviceCollection.AddScoped<ICCAuthenticationService, CCAuthenticationService>();
    }

}
