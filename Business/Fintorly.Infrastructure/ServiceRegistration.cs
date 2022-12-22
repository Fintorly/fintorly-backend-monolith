using Fintorly.Application.Interfaces.Utils;
using Fintorly.Infrastructure.Context;
using Fintorly.Infrastructure.Repositories;
using Fintorly.Infrastructure.Utilities.Services;
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
        services.AddScoped<IAdvertisementRepository, AdvertisementRepository>();
        services.AddScoped<IJwtHelper, JwtHelper>();
        services.AddScoped<IPhoneService, PhoneManager>();
        services.AddScoped<IMailService, MailManager>();
        
        var asd = configuration.GetConnectionString(":ConnectionString");
        var optionsBuilder = new DbContextOptionsBuilder<FintorlyContext>()
            .UseSqlServer("Server=localhost;Database=Fintorly;User=sa;Password=bhdKs3WOp7;");

        services.AddScoped<IAuthRepository, AuthRepository>();
        
        using var dbContext = new FintorlyContext(optionsBuilder.Options, null);
        dbContext.Database.EnsureCreated();
        dbContext.Database.Migrate();

        return services;
    }
}