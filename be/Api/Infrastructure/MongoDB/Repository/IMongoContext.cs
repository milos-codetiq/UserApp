using MongoDB.Driver;

namespace Api.Infrastructure.MongoDB.Repository;

public interface IMongoContext : IDisposable
{
    void AddCommand(Func<Task> func);
    Task<int> SaveChangesAsync();
    IMongoCollection<T> GetCollection<T>(string name);
}
