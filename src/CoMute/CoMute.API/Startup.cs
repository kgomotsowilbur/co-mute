using System;
using AeverPortal.Abstractions;
using AeverPortal.Abstractions.Models;
using AeverPortal.DataProvider.CosmosDb;
using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;

namespace AeverPortal.FrontEndAPI;

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

        // Cosmos
        var cosmosDbSettiongs = new CosmosDbSettings();
        Configuration.Bind(nameof(CosmosDbSettings), cosmosDbSettiongs);
        services.AddSingleton(cosmosDbSettiongs);

        services.AddDbContextFactory<DataContext>((IServiceProvider sp, DbContextOptionsBuilder opts) =>
        {
            opts.UseCosmos(cosmosDbSettiongs.ConnectionString, cosmosDbSettiongs.Database, (dbOptions) =>
            {
                dbOptions.ConnectionMode(ConnectionMode.Direct);
            });


        });

        services.AddScoped<IPortalDataProvider, PortalDataProvider>();
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


        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Aever Api V1");
        });

        //app.MapControllers();

        //app.Run();
        dataProvider.Initialize();
    }
}

