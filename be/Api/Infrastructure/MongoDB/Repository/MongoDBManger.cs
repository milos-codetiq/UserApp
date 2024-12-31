using Api.DomainEntities;

namespace Api.Infrastructure.MongoDB.Repository;

public class MongoDBManger : IMongoDBManger
{
    private readonly IMongoContext _dbContext;

    public MongoDBManger(IMongoContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IRepository<T> Repository<T>() where T : MongoBaseEntity
    {
        return new Repository<T>(_dbContext);
    }

    public async Task<bool> CommitAsync()
    {
        var changeAmount = await _dbContext.SaveChangesAsync();
        return changeAmount > 0;
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }
}
