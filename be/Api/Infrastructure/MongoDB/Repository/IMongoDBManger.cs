using Api.DomainEntities;

namespace Api.Infrastructure.MongoDB.Repository;

public interface IMongoDBManger : IDisposable
{
    Task<bool> CommitAsync();
    IRepository<T> Repository<T>() where T : MongoBaseEntity;
}
