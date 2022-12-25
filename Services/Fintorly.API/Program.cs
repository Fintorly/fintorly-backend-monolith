using System.Reflection;
using MediatR;
using Fintorly.API.Extensions.Registration;
using Fintorly.Application;
using Fintorly.Application.Extensions;
using Fintorly.Infrastructure;
using Fintorly.Infrastructure.Extensions;
using OneSignalApi.Client;
using Fintorly.Domain.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationRegistration();
builder.Services.AddInfrastructureRegistration(builder.Configuration);
builder.Services.ConfigureEventHandlers();

builder.Services.Configure<MailConfiguration>(opts => builder.Configuration.GetSection("SmtpSettings"));
//builder.Services.AddServiceDiscoveryRegistration(builder.Configuration);

string env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

builder.Configuration.SetBasePath(System.IO.Directory.GetCurrentDirectory())
    .AddJsonFile($"Configurations/appsettings.json", optional: false)
    .AddJsonFile($"Configurations/appsettings.{env}.json", optional: true)
    .AddJsonFile($"Configurations/serilog.json", optional: true)
    .AddJsonFile($"Configurations/serilog.{env}.json", optional: true)
    .AddEnvironmentVariables()
    .Build();

builder.Services
    .AddLogging(configure => configure.AddConsole());
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();