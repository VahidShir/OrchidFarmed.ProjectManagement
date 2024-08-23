
using Asp.Versioning;

using FluentValidation;

using Microsoft.EntityFrameworkCore;

using OrchidFarmed.ProjectManagement.Application.Contracts.Commands;
using OrchidFarmed.ProjectManagement.Domain.Repositories;
using OrchidFarmed.ProjectManagement.Infrastructure.Persistence;
using OrchidFarmed.ProjectManagement.Infrastructure.Persistence.Repositories;

using System.Reflection;

namespace OrchidFarmed.ProjectManagement.WebApi;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var configuration = builder.Configuration;
        var services = builder.Services;

        // Add services to the container.

        services.AddDbContext<AppDbContext>(options =>
                                    options.UseSqlServer(configuration.GetConnectionString("Default")));

        services.AddScoped<IProjectRepository, ProjectRepository>();

        ConfigApiVersioning(services);

        services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            // see https://github.com/domaindrivendev/Swashbuckle.AspNetCore/issues/1607
            options.CustomSchemaIds(type => type.ToString());
        });

        ConfigMediatR(services);

        services.AddValidatorsFromAssembly(GetApplicationProjectAssembly());

        var app = builder.Build();

        //automatic db migrations by running the app
        await MigrateDatabase(app.Services);

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }

    private static void ConfigMediatR(IServiceCollection services)
    {
        services.AddMediatR(cf => cf.RegisterServicesFromAssemblies(typeof(CreateProjectCommand).Assembly
            , GetApplicationProjectAssembly()));
    }

    private static Assembly GetApplicationProjectAssembly()
    {
        var applicationProjectAssemblyPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "OrchidFarmed.ProjectManagement.Application.dll");
        var applicationProjectAssembly = Assembly.LoadFile(applicationProjectAssemblyPath);

        return applicationProjectAssembly;
    }

    private static async Task MigrateDatabase(IServiceProvider service)
    {
        using (var serviceScope = service.CreateScope())
        {
            var dbContext = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
            await dbContext.Database.MigrateAsync();
        }
    }

    private static void ConfigApiVersioning(IServiceCollection services)
    {
        services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1);
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader());
            })
            .AddMvc() // This is needed for controllers
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'V";
                options.SubstituteApiVersionInUrl = true;
            });
    }
}
