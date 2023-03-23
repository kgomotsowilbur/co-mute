//using AeverPortal.FrontEndAPI;

//var builder = WebApplication.CreateBuilder(args);

//var startup = new Startup(builder.Configuration);
//startup.ConfigureServices(builder.Services); // calling ConfigureServices method

//var app = builder.Build();
//startup.Configure(app, builder.Environment); // calling Configure method
using AeverPortal.FrontEndAPI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;


namespace AEVERPortal.FrontEndAPI;
public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}

