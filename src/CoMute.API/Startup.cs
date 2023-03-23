using System;
using CoMute.Abstractions;
using CoMute.Abstractions.Models;
using CoMute.DataProvider;
using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;

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
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "CoMute Api V1");
        });

        //app.MapControllers();

        //app.Run();
        dataProvider.Initialize();
    }
}

