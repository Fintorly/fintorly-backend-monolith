using System.Reflection;
using System.Security.Claims;
using MediatR;
using Fintorly.API.Extensions.Registration;
using Fintorly.Application;
using Fintorly.Infrastructure;
using Fintorly.Domain.Options;
using Fintorly.Infrastructure.Utilities.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
string env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

builder.Configuration.SetBasePath(System.IO.Directory.GetCurrentDirectory())
    .AddJsonFile($"Configurations/appsettings.json", optional: false)
    .AddJsonFile($"Configurations/appsettings.{env}.json", optional: true)
    .AddJsonFile($"Configurations/serilog.json", optional: true)
    .AddJsonFile($"Configurations/serilog.{env}.json", optional: true)
    .AddEnvironmentVariables()
    .Build();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Fintorly.API", Version = "0.0.1" }); //auth i�ini ��zd�m olm
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme.
                     Enter 'Bearer' [space] and then your token in the text input below.
                      Example: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {{
            new OpenApiSecurityScheme
            {
                Reference=new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                },
                Scheme="oauth2",
                Name="Bearer",
                In=ParameterLocation.Header,
            },
            new List<string>() }
    });
});

var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = false,
        ValidIssuer = tokenOptions.Issuer,
        ValidAudience = tokenOptions.Audience,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
    };

    options.RequireHttpsMetadata = false;
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            var accessToken = context.Request.Query["access_token"];

            // If the request is for our hub...
            var path = context.HttpContext.Request.Path;
            if (!string.IsNullOrEmpty(accessToken) &&
                (path.StartsWithSegments("/portfolio")))
            {
                // Read the token out of the query string
                context.Token = accessToken;
            }
            return Task.CompletedTask;
        }
    };
});


builder.Services.AddAuthorization(options =>
    options.AddPolicy("Role",
        policy => policy.RequireClaim(claimType: ClaimTypes.Role, "Admin", "User","Mentor","Guest" )));

builder.Services.AddApplicationRegistration();
builder.Services.AddInfrastructureRegistration(builder.Configuration);
builder.Services.ConfigureEventHandlers();

// builder.Services.AddHangfire(x => x.UseSqlServerStorage(string.Format(@"Server=176.53.2.38;Database=Hangfire;User Id=Fintorly;Password=Fx3475*;Trusted_Connection=false")));
// builder.Services.AddHangfireServer();
// builder.Services.AddDistributedRedisCache(option =>
// {
//     option.Configuration = "127.0.0.1:6379";
// });

builder.Services.Configure<MailConfiguration>(opts => builder.Configuration.GetSection("SmtpSettings"));
//builder.Services.AddServiceDiscoveryRegistration(builder.Configuration);



builder.Services
    .AddLogging(configure => configure.AddConsole());
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();
//app.UseHangfireDashboard("/dashboard");
app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true) // allow any origin
    .AllowCredentials());

// app.UseStaticFiles(new StaticFileOptions
// {
//     FileProvider = new PhysicalFileProvider(
//         builder.Environment.ContentRootPath + "/wwwroot" + "/Uploads"),
//     //Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads")),
//     RequestPath = new PathString("/Uploads"),
// });
app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();