using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using CoinSight.Client;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using System;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Popups;
using CoinConstraint.Client.Infrastructure.Authentication;
using CoinSight.Client.Util;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddScoped<SfDialogService>();
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MTMwMTQ4OUAzMjMwMmUzNDJlMzBmV2VjTGJHT1ZWMkZlc3ZpUUROSmhoMTZPRFBKbkpjL0UzbWlGcnpic21FPQ==");
builder.Services.AddSyncfusionBlazor();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddTransient<CCAuthenticationHeaderHandler>();

//builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("CoinConstraint.API"));

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
