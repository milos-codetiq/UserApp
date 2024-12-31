using MongoDB.Driver;

namespace Api.Infrastructure.MongoDB.Repository;

public class MongoContext : IMongoContext
{
    public IClientSessionHandle Session { get; set; }

    private readonly IMongoDatabase _mongoDatabase;
    private readonly IMongoClient _mongoClient;
    private readonly List<Func<Task>> _commands;

    public MongoContext(IMongoDatabase mongoDatabase, IMongoClient mongoClient)
    {
        _mongoDatabase = mongoDatabase;
        _mongoClient = mongoClient;
        _commands = new List<Func<Task>>();
    }

    public async Task<int> SaveChangesAsync()
    {
        var count = _commands.Count;

        using (Session = await _mongoClient.StartSessionAsync())
        {
            Session.StartTransaction();
            var commandTasks = _commands.Select(c => c());
            await Task.WhenAll(commandTasks);
            await Session.CommitTransactionAsync();
            _commands.Clear();
        }

        return count;
    }

    public IMongoCollection<T> GetCollection<T>(string name)
    {
        return _mongoDatabase.GetCollection<T>(name);
    }

    public void Dispose()
    {
        Session?.Dispose();
        GC.SuppressFinalize(this);
    }

    public void AddCommand(Func<Task> func)
    {
        _commands.Add(func);
    }
}
