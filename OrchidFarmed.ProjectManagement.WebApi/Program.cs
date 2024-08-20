
using Microsoft.EntityFrameworkCore;

using OrchidFarmed.ProjectManagement.Infrastructure.Persistence;

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

        services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

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

    private static async Task MigrateDatabase(IServiceProvider service)
    {
        using (var serviceScope = service.CreateScope())
        {
            var dbContext = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
            await dbContext.Database.MigrateAsync();
        }
    }
}
