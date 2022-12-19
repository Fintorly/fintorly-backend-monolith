using System;
using System.Reflection;
using Fintorly.Application.Interfaces.Utils;
using Fintorly.Application.Utilities;
using Microsoft.AspNetCore.Http;

namespace Fintorly.Application
{
	public static class ServiceRegistration
	{
        public static void AddApplicationRegistration(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());


            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IJwtHelper, JwtHelper>();
            services.AddScoped<IPhoneService, PhoneManager>();
            services.AddScoped<IMailService, MailManager>();

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        }
    }
}

