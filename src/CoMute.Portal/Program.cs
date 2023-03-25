using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using CoMute.Portal;
using MudBlazor.Services;
using CoMute.Portal.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CoMute.Abstractions.Models.Setting;
using CoMute.Portal.Helpers;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

var builder = WebAssemblyHostBuilder.CreateDefault(args);


builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMudServices();
//builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = webApiUrl });

builder.Services.AddHttpClient("CoMute.ServerAPI", client =>
        client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
    .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>()
    .CreateClient("CoMute.ServerAPI"));

builder.Services.AddSingleton<IUserDataService, UserDataService>(serviceProvider => new(serviceProvider.GetRequiredService<HttpClient>()));
builder.Services.AddSingleton<ICarPoolOpportunityDataService, CarPoolOpportunityDataService>(serviceProvider => new(serviceProvider.GetRequiredService<HttpClient>()));

builder.Services.AddApiAuthorization();

await builder.Build().RunAsync();

