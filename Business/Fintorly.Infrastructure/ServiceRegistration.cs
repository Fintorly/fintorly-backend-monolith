using Fintorly.Infrastructure.Context;
using Fintorly.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fintorly.Infrastructure;

public static class ServiceRegistration
{
    public static IServiceCollection AddAInfrastructureRegistration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<FintorlyContext>(opt =>
        {
            opt.UseSqlServer("Server=localhost;Database=Fintorly;User=sa;Password=bhdKs3WOp7;");
            opt.EnableSensitiveDataLogging();
        });

        services.AddScoped<IUserRepository, UserRepository>();
        var asd = configuration.GetConnectionString(":ConnectionString");
        var optionsBuilder = new DbContextOptionsBuilder<FintorlyContext>()
            .UseSqlServer("Server=localhost;Database=Fintorly;User=sa;Password=bhdKs3WOp7;");

        using var dbContext = new FintorlyContext(optionsBuilder.Options, null);
        dbContext.Database.EnsureCreated();
        dbContext.Database.Migrate();

        return services;
    }
}