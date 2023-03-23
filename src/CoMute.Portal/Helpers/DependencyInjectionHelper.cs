using System;
using System.Net.Http.Headers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Toolbelt.Blazor.Extensions.DependencyInjection;

namespace CoMute.Portal.Helpers;

public static class DependencyInjectionHelper
{
    public static void RegisterTypedClient<TClient, TImplementation>(this IServiceCollection services, Uri apiBaseUrl)
          where TClient : class where TImplementation : class, TClient
    {
        services.AddHttpClient<TClient, TImplementation>((sp, client) =>
        {
            client.BaseAddress = apiBaseUrl;
            client.EnableIntercept(sp);
        });
            // .AddHttpMessageHandler<WebApiAuthorizationMessageHandler>();
    }
}



