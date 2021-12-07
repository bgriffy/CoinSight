using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace CoinConstraint.Client;

public class Program
{
    public static async Task Main(string[] args)
    {

        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");

        builder.Services.AddHttpClient("CoinConstraint.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
            .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

        // Supply HttpClient instances that include access tokens when making requests to the server project
        builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("CoinConstraint.ServerAPI"));

        builder.Services.AddThirdPartyServices();
        builder.Services.AddRepositores();
        builder.Services.AddDataAccessServices();
        builder.Services.AddApplicationServices();

        builder.Services.AddApiAuthorization();

        await builder.Build().RunAsync();
    }
}
