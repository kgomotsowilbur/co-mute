using System;
using CoMute.Abstractions;
using CoMute.Abstractions.Models;
using CoMute.DataProvider;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using CoMute.API.Handler;
using Microsoft.AspNetCore.ResponseCompression;

namespace CoMute.API;

public class Startup
{
    public IConfiguration Configuration
    {
        get;
    }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        // Add services to the container.

        services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy",
                builder => builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
        });

        //SQL db
        services.AddDbContextFactory<DataContext>(options =>
            options.UseSqlServer(
                Configuration.GetConnectionString("ConnectionString"),
                x => x.MigrationsAssembly("CoMute.API")
            )
        );

        services.AddTransient<IPortalDataProvider, PortalDataProvider>();

        services.AddMsalAuthentication(options =>
        {
            Configuration.Bind("AzureAd", options.ProviderOptions.Authentication);
            options.ProviderOptions.DefaultAccessTokenScopes.Add("api://d1548452-eff0-4c62-88dc-ffff5bdbc3df/CoMute.API");
            options.ProviderOptions.LoginMode = "redirect";
        });

        services.AddDefaultIdentity<Users>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<DataContext>();

        services.AddIdentityServer()
            .AddApiAuthorization<Users, DataContext>();

        services.AddTransient<GraphAuthorizationMessageHandler>();

        services.AddHttpClient("GraphAPI",
                client => client.BaseAddress = new Uri(
                    Configuration.GetSection("MicrosoftGraph")["BaseUrl"]))
            .AddHttpMessageHandler<GraphAuthorizationMessageHandler>();

        services.Configure<JwtBearerOptions>(
            JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.TokenValidationParameters.NameClaimType = "name";
            });

       services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme);
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IPortalDataProvider dataProvider)
    {
        // Configure the HTTP request pipeline.
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseCors("CorsPolicy");
        }

        app.UseHttpsRedirection();
        app.UseRouting();

        app.UseIdentityServer();
        app.UseAuthentication();
        app.UseAuthorization();

            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "CoMute Api V1");
        });

        //app.Run();
        dataProvider.Initialize();
    }
}

