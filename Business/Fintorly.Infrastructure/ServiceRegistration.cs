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
    public static IServiceCollection AddInfrastructureRegistration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<FintorlyContext>(opt =>
        {
            opt.UseSqlServer("Server=localhost;Database=Fintorly;User=sa;Password=bhdKs3WOp7;");
            opt.EnableSensitiveDataLogging();
        });

        services.AddScoped<IJwtHelper, JwtHelper>();
        services.AddScoped<IPhoneService, PhoneManager>();
        services.AddScoped<IMailService, MailManager>();
        services.AddScoped<ITokenResolver, TokenResolver>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IAdvertisementRepository, AdvertisementRepository>();
        services.AddScoped<IAuthRepository, AuthRepository>();
        services.AddScoped<IUserAuthRepository, UserAuthRepository>();
        services.AddScoped<IMentorAuthRepository, MentorAuthRepository>();
        services.AddScoped<IPortfolioRepository,PortfolioRepository>();
        services.AddScoped<IQuestionRepository, QuestionRepository>();
        services.AddScoped<IAnswerRepository, AnswerRepository>();
        services.AddScoped<IProfilePictureRepository, ProfilePictureRepository>();
        
        var asd = configuration.GetConnectionString(":ConnectionString");
        var optionsBuilder = new DbContextOptionsBuilder<FintorlyContext>()
            .UseSqlServer("Server=localhost;Database=Fintorly;User=sa;Password=bhdKs3WOp7;");

        services.AddScoped<IUserAuthRepository, UserAuthRepository>();
        
        using var dbContext = new FintorlyContext(optionsBuilder.Options, null);
        dbContext.Database.EnsureCreated();
        dbContext.Database.Migrate();

        return services;
    }
}