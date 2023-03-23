using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using AeverPortal.Portal;
using MudBlazor.Services;
using AeverPortal.Portal.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AeverPortal.Abstractions.Models.Setting;
using AeverPortal.Portal.Helpers;



var builder = WebAssemblyHostBuilder.CreateDefault(args);
var webApiConfig = builder.Configuration.GetSection("PortalConfiguration").Get<PortalConfigurationSettings>();
var webApiUrl = new Uri(webApiConfig.WebApi.Url);


builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMudServices();
builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = webApiUrl });

//builder.Services.RegisterTypedClient<IUserDataService, UserDataService>(webApiUrl);
builder.Services.AddSingleton<ITeamMemberDataService, TeamMemberDataService>(serviceProvider => new(serviceProvider.GetRequiredService<HttpClient>()));
builder.Services.AddSingleton<IFundManagerDataService, UserDataService>(serviceProvider => new(serviceProvider.GetRequiredService<HttpClient>()));
builder.Services.AddSingleton<ICarPoolOpportunityDataService, CarPoolOpportunityDataService>(serviceProvider => new(serviceProvider.GetRequiredService<HttpClient>()));

await builder.Build().RunAsync();

