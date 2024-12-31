using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Api.DomainEntities;

namespace Api.Infrastructure.MongoDB.Repository;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : MongoBaseEntity
{
    private readonly IMongoContext _dbContext;
    protected IMongoCollection<TEntity> dbSet;

    public Repository(IMongoContext context)
    {
        _dbContext = context;
        dbSet = _dbContext.GetCollection<TEntity>(typeof(TEntity).Name);
    }

    private FilterDefinition<TEntity> BuildIdFilter(TEntity obj)
    {
        FilterDefinition<TEntity> filter = Builders<TEntity>.Filter.Eq("_id", obj.Id);
        return filter;
    }
    private FilterDefinition<TEntity> BuildIdFilter(Guid id)
    {
        FilterDefinition<TEntity> filter = Builders<TEntity>.Filter.Eq("_id", id);
        return filter;
    }

    public void Add(TEntity obj)
    {
        _dbContext.AddCommand(() => dbSet.InsertOneAsync(obj));
    }

    public void Update(TEntity obj)
    {
        var filter = BuildIdFilter(obj);
        _dbContext.AddCommand(() => dbSet.ReplaceOneAsync(filter, obj));
    }

    public void Remove(TEntity obj)
    {
        var filter = BuildIdFilter(obj);
        _dbContext.AddCommand(() => dbSet.DeleteOneAsync(filter));
    }

    public void Upsert(TEntity obj)
    {
        var filter = BuildIdFilter(obj);
        var options = new ReplaceOptions() { IsUpsert = true };
        _dbContext.AddCommand(() => dbSet.ReplaceOneAsync(filter, obj, options));
    }

    public async Task<TEntity> GetById(Guid id)
    {
        var filter = BuildIdFilter(id);
        var data = await dbSet.FindAsync(filter);
        return data.SingleOrDefault();
    }

    public async Task<IEnumerable<TEntity>> GetAll()
    {
        var all = await dbSet.FindAsync(Builders<TEntity>.Filter.Empty);
        return all.ToList();
    }

    public IMongoQueryable<TEntity> QueryAll()
    {
        var query = dbSet.AsQueryable();
        return query;
    }

    public async Task<BulkWriteResult<TEntity>> BulkUpsert(List<TEntity> records)
    {
        var bulk = new List<WriteModel<TEntity>>();

        foreach (var record in records)
        {
            var filter = BuildIdFilter(record);
            var upsertOne = new ReplaceOneModel<TEntity>(filter, record) { IsUpsert = true };
            bulk.Add(upsertOne);
        }

        var result = await dbSet.BulkWriteAsync(bulk);
        return result;
    }

    public void Dispose()
    {
        _dbContext?.Dispose();
    }
}
