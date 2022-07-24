using CoinConstraint.Client.Infrastructure.Authentication;
using Syncfusion.Blazor;

namespace CoinConstraint.Client;

public class Program
{
    public static async Task Main(string[] args)
    {

        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");

        var syncfusionLicenseKey = builder.Configuration["SyncfusionLicenseKey"];
        Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(syncfusionLicenseKey);

        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

        builder.Services.AddTransient<CCAuthenticationHeaderHandler>();

        builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("CoinConstraint.API"));

        builder.Services.AddSyncfusionBlazor(options => { options.IgnoreScriptIsolation = true; });

        builder.Services.AddHttpClient("CoinConstraint.API", client =>
        {
            client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
        }).AddHttpMessageHandler<CCAuthenticationHeaderHandler>();

        builder.Services.AddThirdPartyServices();
        builder.Services.AddRepositores();
        builder.Services.AddDataAccessServices();
        builder.Services.AddApplicationServices();
        builder.Services.AddAuthServices();

        await builder.Build().RunAsync();
    }
}
