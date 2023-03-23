using CoMute.Abstractions.Models.Setting;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace CoMute.Portal.Helpers;

public class WebApiAuthorizationMessageHandler : AuthorizationMessageHandler
{
    public WebApiAuthorizationMessageHandler(IAccessTokenProvider provider, NavigationManager navigationManager, PortalConfigurationSettings configurationSettings)
    : base(provider, navigationManager)
    {
        ConfigureHandler(
            authorizedUrls: new[] { configurationSettings.WebApi.Url },
            scopes: new[] { configurationSettings.WebApi.Audience });
    }
}