using Blazor.ModalDialog;
using Blazored.LocalStorage;
using CoinConstraint.Application.Identity;
using CoinConstraint.Client.Infrastructure.Authentication;
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
    }

    public static void AddApplicationServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IBudgetingService, BudgetingService>();
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
