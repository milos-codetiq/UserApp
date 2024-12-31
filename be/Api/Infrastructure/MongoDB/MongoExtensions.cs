using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver.Core.Events;
using Api.Common.Models.Settings;

namespace Api.Infrastructure.MongoDB;

public static class MongoExtensions
{
    public static void ConfigureConventions()
    {
        var pack = new ConventionPack
        {
            new IgnoreExtraElementsConvention(true),
            new IgnoreIfDefaultConvention(false)
        };

        ConventionRegistry.Register("VBAI Database Conventions", pack, t => true);
    }

    static MongoClient GetClient(string connectionString)
    {
        var url = new MongoUrl(connectionString);
        var settings = MongoClientSettings.FromUrl(url);

        settings.ClusterConfigurator = cb =>
        {
            cb.Subscribe<CommandStartedEvent>(e =>
            {
                string command = e.Command.ToJson();
                System.Diagnostics.Debug.WriteLine($"Executing mongo command: {e.CommandName}");
                System.Diagnostics.Debug.WriteLine(command);
            });
        };

        var client = new MongoClient(settings);

        return client;
    }

    public static void AddMongoServices(this IServiceCollection services, IConfiguration configuration)
    {
        ConfigureConventions();

        var mongoSettings = configuration.GetSection("MongoDBSettings").Get<MongoDBSettings>();

        IMongoClient client = GetClient(mongoSettings.ConnectionString);
        services.AddSingleton(client);

        IMongoDatabase database = client.GetDatabase(mongoSettings.DatabaseName);
        services.AddSingleton(database);

        services.AddScoped<IMongoContext, MongoContext>();
        services.AddScoped<IMongoDBManger, MongoDBManger>();
    }
}