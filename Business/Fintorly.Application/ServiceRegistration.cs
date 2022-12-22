using System;
using System.Reflection;
using Fintorly.Application.Interfaces.Utils;
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
    

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        }
    }
}

