using Microsoft.OpenApi.Models;
using System.Reflection;
using Api.Infrastructure.MongoDB;
using Api.Features;


namespace Api.Infrastructure;

public static class ServiceCollectionsExtensions
{
    public static void AddProjectServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
        builder.Services.AddMongoServices(builder.Configuration);

        builder.Services.AddScoped<IntegrationService>();
    }

    public static void AddInfrastructure(this WebApplicationBuilder builder)
    {
        builder.AddCloudWatchLogger();

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddCors(options => options.AddDefaultPolicy(
                policy => policy.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod()));

        builder.Services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "Energy Bank User App API", Version = "v1" }));

        builder.Services.AddHttpClient();
    }

    public static void AddCloudWatchLogger(this WebApplicationBuilder builder)
    {
        //TODO: add logic for CW AWS Integration
    }
}